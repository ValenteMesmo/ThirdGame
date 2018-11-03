using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public interface IGetDrawingModels
    {
        IEnumerable<DrawingModel> GetDrawingModels();
    }
}
