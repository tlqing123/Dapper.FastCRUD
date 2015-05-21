﻿namespace Dapper.FastCrud.Providers.SqLite
{
    using System;
    using System.Globalization;
    using System.Linq;

    internal class EntityDescriptor<TEntity>: FastCrud.EntityDescriptor<TEntity>
    {
        private readonly string _tableName;

        public EntityDescriptor()
        {
            if (this.KeyPropertyDescriptors.Length > 1)
            {
                throw new NotSupportedException($"Entity <{typeof(TEntity).Name}> has more than one primary keys. This is not supported by SqLite.");    
            }

            this._tableName = string.Format(CultureInfo.InvariantCulture, "{0}", this.TableDescriptor.Name);

            this.SingleDeleteOperation = new SingleDeleteEntityOperationDescriptor<TEntity>(this);
            this.SingleInsertOperation = new SingleInsertEntityOperationDescriptor<TEntity>(this);
            this.SingleUpdateOperation = new SingleUpdateEntityOperationDescriptor<TEntity>(this);
            this.BatchSelectOperation = new BatchSelectEntityOperationDescriptor<TEntity>(this);
            this.SingleSelectOperation = new SingleSelectEntityOperationDescriptor<TEntity>(this);        
        }

        public override string TableName
        {
            get
            {
                return _tableName;
            }
        }
    }
}