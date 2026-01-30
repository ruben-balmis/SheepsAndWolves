using Microsoft.AspNetCore.Mvc;
using SheepsAndWolfsEngine;

namespace SheepsAndWolfs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorldController : ControllerBase
    {
        [HttpGet("objects")]
        public ObjectInfo[] GetWorldObjects()
        {
            World world = DataModel.Instance.World;
            var result = Utils.ToObjectsInfo(world);
            world.RunTurn();
            return result;
        }
        [HttpGet("sizes")]  // Responde a /world/sizes
        public WorldInfo GetWorldSizes()
        {
            World world = DataModel.Instance.World;
            var result = new WorldInfo(world.Width, world.Height);
            return result;
        }
    }
}
