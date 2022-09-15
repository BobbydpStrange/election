using election.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace election.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase

    {
        private readonly InstantRunoffContext instantRunoffContext;

        public DatabaseController(InstantRunoffContext instantRunoffContext)
        {
            this.instantRunoffContext = instantRunoffContext;
        }

        [HttpGet("[action]")]
        public async Task <int> RowCount()
        {
            /*//get number of rows in a table
            return -1;*/
            //get number of rows in a table
            var rowCount = await instantRunoffContext.Ballots.CountAsync();
            return rowCount;
        }
    }
}
