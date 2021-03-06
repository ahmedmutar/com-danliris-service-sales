﻿using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceItemModel : BaseModel
    {
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }

        #region Uom
        public int UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }
        #endregion

        public double Total { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int SalesInvoiceDetailId { get; set; }

        public virtual SalesInvoiceDetailModel SalesInvoiceDetailModel { get; set; }
    }
}
