
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;

using CQRS;

namespace Model
{
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class AddCategory : Command
	{
		public AddCategory (string id, string title, string parentId)
		{
			Id = id;
			Title = title;
			ParentId = parentId;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
		public string ParentId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class CategoryAdded : Event
	{
		public CategoryAdded (string id, string title, string parentId)
		{
			Id = id;
			Title = title;
			ParentId = parentId;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
		public string ParentId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class ChangeCategoryTitle : Command
	{
		public ChangeCategoryTitle (string id, string title)
		{
			Id = id;
			Title = title;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class CategoryTitleChanged : Command
	{
		public CategoryTitleChanged (string id, string title)
		{
			Id = id;
			Title = title;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class AddProduct : Command
	{
		public AddProduct (string id, string title, decimal price, string parentCategoryId)
		{
			Id = id;
			Title = title;
			Price = price;
			ParentCategoryId = parentCategoryId;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
		public decimal Price { get; private set; }
		public string ParentCategoryId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductAdded : Event
	{
		public ProductAdded (string id, string title, decimal price, string parentCategoryId)
		{
			Id = id;
			Title = title;
			Price = price;
			ParentCategoryId = parentCategoryId;
		}
		
		public string Id { get; private set; }
		public string Title { get; private set; }
		public decimal Price { get; private set; }
		public string ParentCategoryId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductAddedToCategory : Event
	{
		public ProductAddedToCategory (string categoryId)
		{
			CategoryId = categoryId;
		}
		
		public string CategoryId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductRemovedFromCategory : Event
	{
		public ProductRemovedFromCategory (string categoryId)
		{
			CategoryId = categoryId;
		}
		
		public string CategoryId { get; private set; }
	}
	
	[GeneratedCode("MessagesGenerator", "1.0.0.0")]
	public sealed class ProductCategoryChanged : Event
	{
		public ProductCategoryChanged (string oldCategoryId, string newCategoryId)
		{
			OldCategoryId = oldCategoryId;
			NewCategoryId = newCategoryId;
		}
		
		public string OldCategoryId { get; private set; }
		public string NewCategoryId { get; private set; }
	}

}
