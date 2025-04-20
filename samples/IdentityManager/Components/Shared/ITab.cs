using Microsoft.AspNetCore.Components;

namespace IdentityManager.Shared;

public interface ITab
{
    RenderFragment ChildContent { get; }
}