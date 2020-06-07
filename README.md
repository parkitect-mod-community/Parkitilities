# Parkitilities

A Utility library of [Fluent API's](https://en.wikipedia.org/wiki/Fluent_interface) that make building custom Parkitect objects as easy as pie. 

# Design goal

The goal of Parkitilities is to provide easy-to-use, feature-rich library that allows
for the creation of custom parkitect objects. Assets from the [ParkitectAssetEditor](https://github.com/Parkitect/ParkitectAssetEditor)
can be directly imported, modified, substituted and tweaked.

## Support
- Decos
- Vehicle
    - Car
- Entertainer - TODO
- Train
- Fence - TODO
- Bench - TODO
- Wall - TODO
- Sign - TODO
- FlatRide - TODO
- TrackedRides - TODO
- Lamp - TODO
- Tv - TODO
- ImageSign - TODO
- Trashbin - TODO
- Shops - TODO

# Decos

```C#
Parkitility.CreateDefault<Deco>(go)
    .Id("<unique-id>")
    .EnableLightsOnAtNight
    .CustomColor(Color.blue)
    .DisplayName("Display Name")
    .Build(loader);
```




 
