﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace SportsStore.Domain.Context
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SportsStoreEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new SportsStoreEntities object using the connection string found in the 'SportsStoreEntities' section of the application configuration file.
        /// </summary>
        public SportsStoreEntities() : base("name=SportsStoreEntities", "SportsStoreEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SportsStoreEntities object.
        /// </summary>
        public SportsStoreEntities(string connectionString) : base(connectionString, "SportsStoreEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SportsStoreEntities object.
        /// </summary>
        public SportsStoreEntities(EntityConnection connection) : base(connection, "SportsStoreEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Products> Products
        {
            get
            {
                if ((_Products == null))
                {
                    _Products = base.CreateObjectSet<Products>("Products");
                }
                return _Products;
            }
        }
        private ObjectSet<Products> _Products;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Products EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProducts(Products products)
        {
            base.AddObject("Products", products);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="SportsStoreModel", Name="Products")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Products : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Products object.
        /// </summary>
        /// <param name="productID">Initial value of the ProductID property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="category">Initial value of the Category property.</param>
        /// <param name="price">Initial value of the Price property.</param>
        public static Products CreateProducts(global::System.Int32 productID, global::System.String name, global::System.String description, global::System.String category, global::System.Decimal price)
        {
            Products products = new Products();
            products.ProductID = productID;
            products.Name = name;
            products.Description = description;
            products.Category = category;
            products.Price = price;
            return products;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                if (_ProductID != value)
                {
                    OnProductIDChanging(value);
                    ReportPropertyChanging("ProductID");
                    _ProductID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ProductID");
                    OnProductIDChanged();
                }
            }
        }
        private global::System.Int32 _ProductID;
        partial void OnProductIDChanging(global::System.Int32 value);
        partial void OnProductIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Category
        {
            get
            {
                return _Category;
            }
            set
            {
                OnCategoryChanging(value);
                ReportPropertyChanging("Category");
                _Category = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Category");
                OnCategoryChanged();
            }
        }
        private global::System.String _Category;
        partial void OnCategoryChanging(global::System.String value);
        partial void OnCategoryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private global::System.Decimal _Price;
        partial void OnPriceChanging(global::System.Decimal value);
        partial void OnPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] ImageData
        {
            get
            {
                return StructuralObject.GetValidValue(_ImageData);
            }
            set
            {
                OnImageDataChanging(value);
                ReportPropertyChanging("ImageData");
                _ImageData = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ImageData");
                OnImageDataChanged();
            }
        }
        private global::System.Byte[] _ImageData;
        partial void OnImageDataChanging(global::System.Byte[] value);
        partial void OnImageDataChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ImageMimeType
        {
            get
            {
                return _ImageMimeType;
            }
            set
            {
                OnImageMimeTypeChanging(value);
                ReportPropertyChanging("ImageMimeType");
                _ImageMimeType = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ImageMimeType");
                OnImageMimeTypeChanged();
            }
        }
        private global::System.String _ImageMimeType;
        partial void OnImageMimeTypeChanging(global::System.String value);
        partial void OnImageMimeTypeChanged();

        #endregion
    
    }

    #endregion
    
}