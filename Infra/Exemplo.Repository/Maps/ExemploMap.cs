using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Exemplo.Domain.Entities;

namespace Exemplo.Repository.Maps
{   
    class ExemploMap : IEntityTypeConfiguration<Exemplo>
    {
        public void Configure(EntityTypeBuilder<Exemplo> builder)
        {
            //builder.ToTable("Empresa", "uc");
            //builder.HasKey(e => e.Id);
            //builder.Property(e => e.Id).HasColumnName("partnerId").HasColumnType("int").HasMaxLength(4);            
            //builder.Property(e => e.RazaoSocial).HasColumnName("RazaoSocial").HasColumnType("varchar").HasMaxLength(1024);
            //builder.Property(e => e.NomeFantasia).HasColumnName("NomeFantasia").HasColumnType("varchar").HasMaxLength(512);
            //builder.Property(e => e.Cnpj).HasColumnName("Cnpj").HasColumnType("varchar").HasMaxLength(14);
            //builder.Property(e => e.InscricaoEstadual).HasColumnName("InscricaoEstadual").HasColumnType("varchar").HasMaxLength(32);            
            //builder.Property(e => e.Alias).HasColumnName("Alias").HasColumnType("varchar").HasMaxLength(256);
            //builder.Property(e => e.CodigoIntegracao).HasColumnName("CodigoIntegracao").HasColumnType("varchar").HasMaxLength(128);                        
        }
    }
}







