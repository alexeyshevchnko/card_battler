using System.Threading.Tasks;

namespace Shared.Helper{

    public static class HelperTask {

        public static async Task WaitForSeconds(int sec) {
            await Task.Delay(sec * 1000);
        }
    }
}