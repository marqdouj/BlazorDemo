using AspireDemo.PIMS.Models.Canvas;
using Microsoft.JSInterop;

namespace AspireDemo.PIMS.Models
{
    internal class PipeViewJsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/AspireDemo.PIMS/pipeView.js").AsTask());

        public async ValueTask DrawPipeAsync(PipeDisplay pipe)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("drawPipe", pipe);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
