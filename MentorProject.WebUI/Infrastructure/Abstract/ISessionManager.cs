﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
	interface ISessionManager
	{
		bool IsAuthorized { get; set;}
	}
}
