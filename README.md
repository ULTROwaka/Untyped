## UntypedCollection

Collection that can store different types at the same time.
<a href="https://www.nuget.org/packages/NikitaBokov.Util.Untyped/">nuget.org</a>

---


## How to

First of all go to <a href="https://www.nuget.org/packages/NikitaBokov.Util.Untyped/">nuget.org</a> and get the package.

Next import with
```c#
using Untyped;
```

Create instance of collection.
```c#
UntypedCollection storage = new UntypedCollection();
```


Ok, now we need add something to our collection
```c#
//Int
storage.Add(13);
storage.Add(37);
//String
storage.Add("Ivan");
storage.Add("Boris");
//Float
storage.Add(17.5f);
storage.Add(19.3f);
storage.Add(21.7f);
//Custom class
storage.Add(new User(1336, "Alexsey", new DateOnly(1996, 6, 9)));
storage.Add(new User(1337, "Pavel", new DateOnly(1997, 7, 10)));
storage.Add(new User(1338, "Sergey", new DateOnly(1998, 8, 11)));
storage.Add(new User(1339, "Fedor", new DateOnly(1999, 9, 12)));
```
You can see how it easy! Just use Add() method and put any item in it.

Also for custom class is useful to set id selector
```c#
storage.SetId<User, int>(x => x.Id);
```
It help us when we try to get item back. Note a generic argument: first - stored type, second - type of id.

Now try get items.
```c#
//Get all items by type
var ints = storage.Get<int>();
var strings = storage.Get<string>();
var floats = storage.Get<float>();
var users = storage.Get<User>();
//Get item by id (SetId method must be used before)
var users1337 = storage.Get<User,int>(1337);
//return user Pavel 07.10.1997
```

Actually items also can be removed.
```c#
storage.Remove(13);
storage.Remove("Ivan");
storage.Remove("Boris");
storage.Remove(21.7f);
//Remove by id
storage.Remove<User,int>(1337);

```

Like a cool bonus, you can subscribe on adding new items in collection
```c#
//you can use System.Reactive for more flexible usage of subscription
storage.GetSubscriber<string>().Subscribe(Console.WriteLine);
storage.Add("Kirill");
/*
* Output:
* Kirill
*/
```


That's all, folks!
