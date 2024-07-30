﻿using LibraRestaurant.PaymentService.PaymentRequests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace LibraRestaurant.PaymentService.EntityFrameworkCore
{
    public static class PaymentServiceDbContextModelCreatingExtensions
    {
        public static void ConfigurePaymentService(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<PaymentRequest>(entity =>
            {
                entity.ConfigureByConvention();

                entity
                    .Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(PaymentRequestConsts.MaxCurrencyLength);

                entity
                    .Property(p => p.OrderId)
                    .IsRequired()
                    .HasMaxLength(PaymentRequestConsts.MaxOrderIdLength);

                entity.Property(q => q.OrderNo).IsRequired();

                entity.HasIndex(o => o.OrderId);
            });

            builder.Entity<PaymentRequestProduct>(entity =>
            {
                entity.ConfigureByConvention();

                entity
                    .Property(p => p.Code)
                    .IsRequired()
                    .HasMaxLength(PaymentRequestConsts.MaxCodeLength);

                entity
                    .Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(PaymentRequestConsts.MaxNameLength);

                entity.HasOne<PaymentRequest>().WithMany(q => q.Products).HasForeignKey(fk => fk.PaymentRequestId).IsRequired();
            });

        }
    }
}
