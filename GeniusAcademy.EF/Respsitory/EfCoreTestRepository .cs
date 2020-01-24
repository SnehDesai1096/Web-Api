using GeniusAcademy.Data;
using GeniusAcademy.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeniusAcademy.EF.Respsitory
{
    public class EfCoreTestRepository : EfCoreRepository<Test, DataContext>
    {
        public EfCoreTestRepository(DataContext context) : base(context)
        {

        }

       
    }
}