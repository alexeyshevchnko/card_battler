using System.Threading.Tasks;
using Game.Model.Interface;
using Game.Model.Source; 

namespace Game.Controller{
    
    public class BattleController {
        private ISource _source;
        public ISource Source => _source;

        public BattleController() {
            _source = new LocalSource();
        }

        public async Task<bool> TryConnect() {
            return await _source.Connect();
        }
    }
    
}
