using DIMS.EF.DAL.Data.Interfaces;

namespace DIMS.EF.DAL.Data.Services
{
    public class ProcedureManager : IProcedureManager
    {
        private readonly DIMSDBContext _dimsDbContext;

        public ProcedureManager(string connectionString)
        {
            _dimsDbContext = new DIMSDBContext(connectionString);
        }

        public int GetSampleEntriesAmount(bool isAdmin)
        {
            //ObjectParameter result = new ObjectParameter("result", typeof(int));
            int result = 0;
            _dimsDbContext.GetSampleEntriesAmount(isAdmin, ref result);
            return result;
        }
    }
}
