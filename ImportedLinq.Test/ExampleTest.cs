using System;
using System.Collections.Generic;
using ImportedLinq;
using Xunit;

namespace ImportedLinqTest
{
    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }

    enum MonsterType
    {
        Grass,
        Fire,
        Water,
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

    struct Monster
    {
        public int Level { get; }
        public MonsterType MonsterType { get; }

        public Monster(int level, MonsterType monsterType) => (Level, MonsterType) = (level, monsterType);
    }

    public class ExampleTest
    {
        [Fact]
        public void FlattenExample()
        {
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
        }

        [Fact]
        public void BufferExample()
        {
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
        }

        [Fact]
        public void CountByExample()
        {
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
        }

        [Fact]
        public void IsEmptyExample()
        {
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
        }


        [Fact]
        public void MaxByExample()
        {
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
        }

        [Fact]
        public void MinByExample()
        {
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
                new Monster(5, MonsterType.Grass),
                new Monster(5, MonsterType.Fire),
                new Monster(5, MonsterType.Water),
            };

            IReadOnlyCollection<Monster> actual = source.MinBy(it => it.Level);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PartitionExample()
        {
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
        }

        [Fact]
        public void ScanExample()
        {
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
        }

        [Fact]
        public void WithIndexExample()
        {
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
        }

        [Fact]
        public void ZipExample()
        {
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
        }
    }
}