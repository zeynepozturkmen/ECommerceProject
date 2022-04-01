﻿using ECommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Data.Configuration
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //Primary key belirtiliyor
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
        }
    }
}
