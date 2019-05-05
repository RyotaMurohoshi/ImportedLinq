# ImportedLinq (Beta)

`ImportedLinq` provides utility collection methods that are imported from other programming languages.

## What is ImportedLinq?

`LINQ to Objects` is one of the best language feature in C#. But `LINQ to Objects` does not have some methods and features.

For example,

* `MaxBy` finds the elements that have maximum key value in the sequence by using the default comparer.
* `Buffer` generates a sequence of buffers over the source sequence with specified length.
* `Flatten` converts sequence of sequence to sequence.
* `IsEmpty` determines whether sequence is empty or not.

These methods are common in other programming languages.

`ImportedLinq` consists of LINQ like extension methods that are imported from other programming language such `MaxBy`, `Buffer`, `Flatten` and `IsEmpty`.

`ImportedLinq` provide collections methods that are common in other programming language to C# programmer. And `ImportedLinq` helps programmers who are new to C# but have experience to other programming language.

## Project Status

`ImportedLinq` is beta version. Breaking changes may be occur.


## Requirements

`ImportedLinq` needs .NET Standard 1.0.

If you use `ImportedLinq` in Unity, Please download `unitypackage` from [release page](https://github.com/RyotaMurohoshi/ImportedLinq/releases)

## How to use ImportedLinq?

* Unity : download `unitypackage` from [release page](https://github.com/RyotaMurohoshi/ImportedLinq/releases) and import it.
* Others : add your project as [NuGet packages](https://www.nuget.org/packages/ImportedLinq/).

Add using directive like next code.

```csharp
using ImportedLinq;
```

Like LINQ methods, use as extension method for `IEnumerable<T>`.

```csharp
IReadOnlyList<Monster> monsters = LoadMonsters();

IReadOnlyCollection<Monster> maxLevelMonsters = monsters.MaxBy(it => it.Level);

IEnumerable<IReadOnlyList<Monster>> bufferedMonsters = monsters.Buffer(8);
```

## Difference from other libraries

There are some great collection method libraries for C#.

This section describes difference between them and `ImportedLinq`.

Please note that  `ImportedLinq` provides utility collection methods that are imported from other Languages. This is `ImportedLinq`'s motto.

### Difference from Ix.NET

[Ix.NET](https://github.com/dotnet/reactive#interactive-extensions)

> The Interactive Extensions (Ix) is a .NET library which extends LINQ to Objects to provide many of the operators available in Rx but targeted for IEnumerable

from [Ix.NET](https://github.com/dotnet/reactive#interactive-extensions) section description.

`Ix.NET` provides many utility methods for `IEnumerable<T>`. But please note that these methods are correspond methods to `Rx.NET` methods.

### Difference from MoreLINQ

[MoreLINQ](https://github.com/morelinq/MoreLINQ)

> LINQ to Objects is missing a few desirable features.
> 
> This project enhances LINQ to Objects with extra methods, in a manner which keeps to the spirit of LINQ.

from [MoreLINQ](https://github.com/morelinq/MoreLINQ)'s README top content.

`MoreLINQ` is great C# collection method library and has many extension collection methods for `IEnumerable<T>`. Some of them are overlapped with `ImportedLinq`.

But `MoreLINQ` does not have methods whose implementation are so simple like `IsEmpty` and `flatten`. See this [issue](https://github.com/morelinq/MoreLINQ/pull/561).

And there are so many useful methods in `MoreLINQ`. Compare `MoreLINQ`, `ImportedLinq` provides only methods that are common in other programming languages.

## Methods

Show example method usage with next code.

```csharp
enum Direction
{
    Up, Right, Down, Left,
}

enum MonsterType
{
    Grass, Fire, Water,
}

struct Monster
{
    public int Level { get; }
    public MonsterType MonsterType { get; }
    public Monster(int level, MonsterType monsterType) => (Level, MonsterType) = (level, monsterType);
}

struct Position
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y) => (X, Y) = (x, y);

    public static Position Move(Position position, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return new Position(position.X, position.Y + 1);
            case Direction.Right: return new Position(position.X + 1, position.Y);
            case Direction.Down: return new Position(position.X, position.Y - 1);
            case Direction.Left: return new Position(position.X - 1, position.Y);
            default: throw new Exception();
        }
    }
}
```

### MaxBy / MinBy

Finds the elements that have maximum / minimum key value in the sequence by using the default comparer.

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IReadOnlyCollection<Monster> expected = new List<Monster>
{
    new Monster(55, MonsterType.Fire),
};

IReadOnlyCollection<Monster> actual = source.MaxBy(it => it.Level);

Assert.Equal(expected, actual);
```

* Groovy : [max](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#max(groovy.lang.Closure)) / [min](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#min(groovy.lang.Closure))
* Scala : [maxBy](https://www.scala-lang.org/api/current/scala/collection/TraversableLike.html#maxBy[B](f:A=%3EB):A) / [minBy](https://www.scala-lang.org/api/current/scala/collection/TraversableLike.html#minBy[B](f:A=%3EB):A)
* Kotlin : [maxBy](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/max-by.html) / [minBy](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/min-by.html)


### Buffer

Generates a sequence of buffers over the source sequence with specified length.

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IEnumerable<IReadOnlyList<Monster>> expected = new[]
{
    new[]
    {
        new Monster(5, MonsterType.Grass),
        new Monster(16, MonsterType.Grass),
        new Monster(32, MonsterType.Grass),
    },
    new[]
    {
        new Monster(5, MonsterType.Fire),
        new Monster(55, MonsterType.Fire),
        new Monster(5, MonsterType.Water),
    },
    new[]
    {
        new Monster(36, MonsterType.Water),
    },
};

IEnumerable<IReadOnlyList<Monster>> actual = source.Buffer(3);

Assert.Equal(expected, actual);
```

* Groovy : [collate](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#collate(int))
* Scala : [grouped](https://www.scala-lang.org/api/current/scala/collection/IterableLike.html#grouped(size:Int):Iterator[Repr])
* Kotlin : [chunked](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/chunked.html)

### Flatten

Converts sequence of sequence to sequence.

```csharp
IEnumerable<IEnumerable<Monster>> source = new List<Monster[]>
{
    new[]
    {
        new Monster(5, MonsterType.Grass),
        new Monster(16, MonsterType.Grass),
        new Monster(32, MonsterType.Grass),
    },
    new[]
    {
        new Monster(5, MonsterType.Fire),
        new Monster(55, MonsterType.Fire),
    },
    new[]
    {
        new Monster(5, MonsterType.Water),
        new Monster(36, MonsterType.Water),
    },
};

IEnumerable<Monster> expected = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IEnumerable<Monster> actual = source.Flatten();

Assert.Equal(expected, actual);
```

* Groovy : [flatten](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/util/Collection.html#flatten())
* Scala : [flatten](https://www.scala-lang.org/api/current/scala/collection/generic/GenericTraversableTemplate.html#flatten[B]:Traversable[B])
* Kotlin : [flatten](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/flatten.html)

### IsEmpty

Determines whether sequence is empty or not.

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

Assert.False(source.IsEmpty());
```

* Java : [isEmpty](https://docs.oracle.com/en/java/javase/11/docs/api/java.base/java/util/Collection.html#isEmpty())
* Groovy : [isEmpty](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#isEmpty())
* Scala : [isEmpty](https://www.scala-lang.org/api/current/scala/collection/generic/GenericTraversableTemplate.html#isEmpty:Boolean)
* Kotlin : [isEmpty](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/is-empty.html)

### Zip

Creates ValueTuple(First, Second) to the corresponding elements of two sequences, produces a sequence of the ValueTuple(First, Second).

```csharp
IEnumerable<Monster> sourceMonsters = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IEnumerable<Position> sourcePositions = new List<Position>
{
    new Position(0, 1),
    new Position(1, 3),
    new Position(4, -1),
};

IEnumerable<(Monster First, Position Second)> actual = sourceMonsters.Zip(sourcePositions);

IEnumerable<(Monster First, Position Second)> expected = new List<(Monster First, Position Second)>
{
    (new Monster(5, MonsterType.Grass), new Position(0, 1)),
    (new Monster(16, MonsterType.Grass), new Position(1, 3)),
    (new Monster(32, MonsterType.Grass), new Position(4, -1)),
};

Assert.Equal(expected, actual);
```

* Scala : [zip](https://www.scala-lang.org/api/current/scala/collection/Iterable.html#zipAll[B](that:Iterable[B],thisElem:A,thatElem:B):Iterable[(A,B)])
* Kotlin : [zip](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/zip.html)

### WithIndex

Creates ValueTuple(Element, Index) to the corresponding element and its index, produces sequence of the ValueTuple(Element, Index).

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IEnumerable<(Monster Element, int Index)> actual = source.WithIndex();

IEnumerable<(Monster Element, int Index)> expected = new List<ValueTuple<Monster, int>>
{
    ValueTuple.Create(new Monster(5, MonsterType.Grass), 0),
    ValueTuple.Create(new Monster(16, MonsterType.Grass), 1),
    ValueTuple.Create(new Monster(32, MonsterType.Grass), 2),
    ValueTuple.Create(new Monster(5, MonsterType.Fire), 3),
    ValueTuple.Create(new Monster(55, MonsterType.Fire), 4),
    ValueTuple.Create(new Monster(5, MonsterType.Water), 5),
    ValueTuple.Create(new Monster(36, MonsterType.Water), 6),
};

Assert.Equal(expected, actual);
```

* Groovy : [withIndex](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#withIndex())
* Scala : [zipWithIndex](https://www.scala-lang.org/api/current/scala/collection/Iterable.html#zipWithIndex:Iterable[(A,Int)])
* Kotlin : [withIndex](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/with-index.html)

### Scan

Generates a accumulated values sequence with scanning the source sequence and applying the accumulator function.

```csharp
IEnumerable<Direction> directions = new[]
{
    Direction.Down,
    Direction.Down,
    Direction.Left,
    Direction.Up,
    Direction.Right,
    Direction.Up
};

Position seed = new Position(0, 0);

IEnumerable<Position> actual = directions.Scan(seed, Position.Move);
IEnumerable<Position> expected = new[]
{
    new Position(0, -1),
    new Position(0, -2),
    new Position(-1, -2),
    new Position(-1, -1),
    new Position(0, -1),
    new Position(0, 0),
};

Assert.Equal(expected, actual);
```

* Scala : [scan](https://www.scala-lang.org/api/current/scala/collection/TraversableLike.html#scan[B%3E:A,That](z:B)(op:(B,B)=%3EB)(implicitcbf:scala.collection.generic.CanBuildFrom[Repr,B,That]):That)

### Partition

Partitions a sequence to Value(True, False) with a predicate, True is satisfied and False is not.

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IEnumerable<Monster> expectedOver30Level = new List<Monster>
{
    new Monster(32, MonsterType.Grass),
    new Monster(55, MonsterType.Fire),
    new Monster(36, MonsterType.Water),
};

IEnumerable<Monster> expectedUnderOrEqual30Level = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
};

(IReadOnlyCollection<Monster> actualOver30Level, IReadOnlyCollection<Monster> actualUnderOrEqual30Level)
    = source.Partition(it => it.Level > 30);

Assert.Equal(expectedOver30Level, actualOver30Level);
Assert.Equal(expectedUnderOrEqual30Level, actualUnderOrEqual30Level);
```

* Scala : [partition](https://www.scala-lang.org/api/current/scala/collection/TraversableLike.html#partition(p:A=%3EBoolean):(Repr,Repr))
* Kotlin : [partition](https://kotlinlang.org/api/latest/jvm/stdlib/kotlin.collections/partition.html)

### CountBy

Creates a Dictionary<TKey, int> from an source sequence according to a specified key selector function and exist counts in source sequence.

```csharp
IEnumerable<Monster> source = new List<Monster>
{
    new Monster(5, MonsterType.Grass),
    new Monster(16, MonsterType.Grass),
    new Monster(32, MonsterType.Grass),
    new Monster(5, MonsterType.Fire),
    new Monster(55, MonsterType.Fire),
    new Monster(5, MonsterType.Water),
    new Monster(36, MonsterType.Water),
};

IReadOnlyDictionary<MonsterType, int> expected = new Dictionary<MonsterType, int>
{
    {MonsterType.Grass, 3},
    {MonsterType.Fire, 2},
    {MonsterType.Water, 2},
};

IReadOnlyDictionary<MonsterType, int> actual = source.CountBy(it => it.MonsterType);

Assert.Equal(expected, actual);
```

* Groovy : [countBy](http://docs.groovy-lang.org/latest/html/groovy-jdk/java/lang/Iterable.html#countBy(groovy.lang.Closure))

## How to Contribute

If you find bug or problem, please create issue in English.

If you have feature request, please create issue with link to other programming language collection method's document in English.

## Author

Ryota Murohoshi is game Programmer in Japan.

* Posts:http://qiita.com/RyotaMurohoshi (JPN)
* Twitter:https://twitter.com/RyotaMurohoshi (JPN)

## License

This library is under MIT License.
