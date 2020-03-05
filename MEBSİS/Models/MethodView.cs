using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postgresql.Manager.Managers;

namespace MEBSİS.Models
{
    public class MethodView
    {
        public GraduateManager graduate = GraduateManager.CreateAsGraduateManager();
    }
}