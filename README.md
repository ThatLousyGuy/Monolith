## Monolith

Monolith is a Windows library that lets you create animations in codebehind in a fluent, somewhat straightforward manner. It's a wrapper around the Windows.UI.Xaml.Media.Animation library that removes as much of the setup as possible.

## Examples

    using Lousy.Mon;

    // Create an ease for use with animations later
    ExponentialEase ExpEaseOut = 
        new ExponentialEase() { EasingMode = EasingMode.EaseOut }; 

    // Animate a UIElement along the X axis from 0 to 40
    Oli.MoveXOf(uiElem).From(0).To(40).For(0.3, OrSo.Secs).Now();

    // Rotate UIElement to 90 degrees 
    EventToken rotating =
        Oli.Rotate(uiElem).To(90).For(0.3, OrSo.Secs).With(ExpEaseOut).Now();
    // Fade the opacity to 0 after the rotation finishes
    Oli.Fade(uiElem).To(0).For(0.3, OrSo.Secs).With(ExpEaseOut).After(rotating);

    // Run arbitrary code after the rotation too
    Oli.Run(() =>
    {
      // Do things here!
    }).After(rotating);

## Setup

### Use Monolith in your project
Check out [Monolith on NuGet](https://www.nuget.org/packages/Monolith/)!
    PM> Install-Package Monolith

### Use and extend Monolith
- Clone this git repo and include the .csproj file in your solution
- Reference Monolith.dll under ../Monolith/bin/ once you compile
- Hack away!

## Known issues
- Classes and methods aren't documented very well right now. It's next on the todo list
- Using .After(double duration, OrSo type) on successively on the same object with the same animation type is broken (the last call overwrites the previous ones) and it requires an architectural change to fix. Use .After(EventToken token, double duration, OrSo type) instead

## Testing
HA
HAHAHA
HAHA

Please contact me if you want to help write tests for this

## Contributing
Tweet me @ThatLousyGuy if you'd like to help contribute! I've only been adding features as I need them, so if you have an idea for a feature that'd be useful, we should chat about it :D
