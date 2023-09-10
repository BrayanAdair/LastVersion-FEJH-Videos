using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEJEHAudiencias.App_Start
{
    public class IdentityModels : IdentityUser
    {

    }
    public class ApplicationDbContext : IdentityDbContext<IdentityModels>
    {

        public ApplicationDbContext() : base("Data Source=BRAYANPC;Initial Catalog=Acusatorio;Integrated Security=True;")
        { }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}