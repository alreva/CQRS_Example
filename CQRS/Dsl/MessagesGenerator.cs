using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.CodeDom.Compiler;
using System.Globalization;
using MessageContracts;


namespace CQRS.Dsl
{
	public class MessagesGenerator : IGenerateCode
	{
	    private readonly string _commandBaseType;
	    private readonly string _eventBaseType;

	    public MessagesGenerator(
            string commandBaseType = "Command",
            string eventBaseType = "Event")
	    {
	        _commandBaseType = commandBaseType;
	        _eventBaseType = eventBaseType;
	    }

	    public void Generate(Context context, IndentedTextWriter writer)
		{
			foreach (Contract contract in context.Contracts)
			{
				writer.WriteLine();
				writer.WriteLine("[GeneratedCode(\"MessagesGenerator\", \"1.0.0.0\")]");
				writer.Write("public sealed class {0}", contract.Name);
				var interfaces = new List<string>();

				if ((contract.Modifier & ContractModifier.CommandInterface) == ContractModifier.CommandInterface)
				{
                    interfaces.Add(String.Format(CultureInfo.InvariantCulture, _commandBaseType, contract.Members.First().Type));
                }

				if ((contract.Modifier & ContractModifier.EventInterface) == ContractModifier.EventInterface)
				{
                    interfaces.Add(String.Format(CultureInfo.InvariantCulture, _eventBaseType, contract.Members.First().Type));
				}
				
                if (interfaces.Any())
				{
					writer.Write(" : {0}", string.Join(", ", interfaces.ToArray()));
				}
				writer.WriteLine();
				writer.WriteLine("{");
				writer.Indent++;

                WriteConstructor(writer, contract);
                writer.WriteLine();
                WriteMembers(contract, writer);

				writer.Indent--;
				writer.WriteLine("}");
			}
		}

	    private void WriteConstructor(IndentedTextWriter writer, Contract contract)
	    {
            writer.Write("public {0} (", contract.Name);
            if (contract.Members.Count > 0)
            {
                WriteParameters(contract, writer);
            }
            writer.WriteLine(")");

            writer.WriteLine("{");
            if (contract.Members.Count > 0)
	        {
	            writer.Indent++;
	            WriteAssignments(contract, writer);
	            writer.Indent--;
	        }
            writer.WriteLine("}");
        }

	    private void WriteAssignments(Contract contract, IndentedTextWriter writer)
		{
			foreach (Member member in contract.Members)
			{
                writer.WriteLine("{0} = {1};", member.Name, GeneratorUtil.ParameterCase(member.Name));
			}
		}

		private void WriteMembers(Contract contract, IndentedTextWriter writer)
		{
			foreach (Member member in contract.Members)
			{
                writer.Write("public {0} {1} ", member.Type, member.Name);
                writer.WriteLine("{ get; private set; }");
            }
		}

	    private void WriteParameters(Contract contract, TextWriter writer)
		{
			bool first = true;
			foreach (Member member in contract.Members)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					writer.Write(", ");
				}
				writer.Write("{0} {1}", member.Type, GeneratorUtil.ParameterCase(member.Name));
			}
		}
	}
}
