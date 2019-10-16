﻿using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class AcceptedROReportLogic : BaseMonitoringLogic<CostCalculationGarment>
    {
        private SalesDbContext dbContext;

        public AcceptedROReportLogic(SalesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override IQueryable<CostCalculationGarment> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments.Where(cc => cc.IsROAccepted);

            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(cc => cc.Section == filter.section);
            }
            if (!string.IsNullOrWhiteSpace(filter.roNo))
            {
                Query = Query.Where(cc => cc.RO_Number == filter.roNo);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyer))
            {
                Query = Query.Where(cc => cc.BuyerBrandCode == filter.buyer);
            }
            if (filter.acceptedDateStart != null)
            {
                Query = Query.Where(cc => cc.ROAcceptedDate >= filter.acceptedDateStart);
            }
            if (filter.acceptedDateEnd != null)
            {
                Query = Query.Where(cc => cc.ROAcceptedDate <= filter.acceptedDateEnd);
            }

            var result = Query.Select(cc => new CostCalculationGarment
            {
                ROAcceptedDate = cc.ROAcceptedDate,
                RO_Number = cc.RO_Number,
                Article = cc.Article,
                BuyerBrandName = cc.BuyerBrandName,
                DeliveryDate = cc.DeliveryDate,
                Quantity = cc.Quantity,
                UOMUnit = cc.UOMUnit,
                ROAcceptedBy = cc.ROAcceptedBy
            });

            return result;
        }

        private class Filter
        {
            public string section { get; set; }
            public string roNo { get; set; }
            public string buyer { get; set; }
            public DateTimeOffset? acceptedDateStart { get; set; }
            public DateTimeOffset? acceptedDateEnd { get; set; }
        }
    }
}
