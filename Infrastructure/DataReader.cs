using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CQRS;
using Infrastructure.ORM;

namespace Infrastructure
{
    public class DtoDataReader : IDtoDataReader
    {
        private readonly IAdoNetConnectionProvider _connectionProvider;
        private readonly ISqlBuilder _sqlBuilder;

        public DtoDataReader(
            IAdoNetConnectionProvider connectionProvider,
            ISqlBuilder sqlBuilder)
        {
            _connectionProvider = connectionProvider;
            _sqlBuilder = sqlBuilder;
        }

        public IEnumerable<TDto> All<TDto>()
        {
            return RunConnected(c => c.Query<TDto>(_sqlBuilder.BuildSelectAllSql<TDto>()));
        }

        // Clause example: dto => dto.Name == "123123"
        public IEnumerable<TDto> Where<TDto>(params Expression<Func<TDto, object>>[] clauses)
        {
            if (!clauses.Any())
            {
                return All<TDto>();
            }

            object parameters;
            var sql = _sqlBuilder.BuildParameterizedSql(out parameters, clauses);

            return RunConnected(c => c.Query<TDto>(sql, parameters));
        }

        private IEnumerable<TDto> RunConnected<TDto>(Func<IDbConnection, IEnumerable<TDto>> func)
        {
            using (var connection = _connectionProvider.CreateAndOpenConnection())
            {
                return func(connection).ToArray();
            }
        }
    }

    public interface ISqlBuilder
    {
        string BuildSelectAllSql<TDto>();
        string BuildParameterizedSql<TDto>(out dynamic parameters, IEnumerable<Expression<Func<TDto, object>>> clauses);
    }

    public class SqlBuilder : ISqlBuilder
    {
        public string BuildSelectAllSql<TDto>()
        {
            return "select * from " + GetDtoTableName<TDto>();
        }

        public string BuildParameterizedSql<TDto>(out dynamic parameters, IEnumerable<Expression<Func<TDto, object>>> clauses)
        {
            var selectAllSql = BuildSelectAllSql<TDto>();

            parameters = new ExpandoObject();

            if (!clauses.Any())
            {
                return selectAllSql;
            }

            var parameterNames = new List<string>();

            foreach (var clauseExpression in clauses)
            {
                var expression = (LambdaExpression)clauseExpression;
                var body = (UnaryExpression)expression.Body;
                var operand = (BinaryExpression)body.Operand;
                var left = (MemberExpression)operand.Left;
                var propertyName = left.Member.Name;

                object propertyValue = GetValue(operand.Right);

                parameterNames.Add(propertyName);

                ((IDictionary<string, object>)parameters)[propertyName] = propertyValue;
            }

            var parametersSql = " where " + string.Join(" and ", parameterNames.Select(pn => string.Format("{0} = @{0}", pn)));
            return selectAllSql + parametersSql;
        }

        private static string GetDtoTableName<TDto>()
        {
            return typeof(TDto).Name;
        }

        private static object GetValue(Expression valueExpression)
        {
            return Expression
                .Lambda<Func<object>>(Expression.Convert(valueExpression, typeof(object)))
                .Compile()();
        }
    }

    public interface IDtoDataReader
    {
        IEnumerable<TDto> All<TDto>();
        IEnumerable<TDto> Where<TDto>(params Expression<Func<TDto, object>>[] clauses);
    }
}
