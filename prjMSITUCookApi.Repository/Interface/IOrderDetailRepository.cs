﻿using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        public OrderDetailDataModel Get(int id);
    }
}
