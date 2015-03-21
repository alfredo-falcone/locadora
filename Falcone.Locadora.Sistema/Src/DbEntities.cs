using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Falcone.Locadora.Sistema.Data
{
  public partial class DbEntities
  {
    protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
    {
      if (!(entityEntry.Entity is Sequencial) && (int)entityEntry.CurrentValues["Id"] == 0)
      {
        Falcone.Locadora.Sistema.Src.Util.VerificarSequencial(entityEntry);
        Falcone.Locadora.Sistema.Src.Util.PreencherDateTimeMinValue(entityEntry);
      }

      return base.ValidateEntity(entityEntry, items);
    }
  }
}
