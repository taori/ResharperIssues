namespace RIDER_60503
{
	public readonly struct CustomVector3
	{
		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}

		public bool Equals(CustomVector3 other)
		{
			return X == other.X && Y == other.Y && Z == other.Z;
		}

		public override bool Equals(object obj)
		{
			return obj is CustomVector3 other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X;
				hashCode = (hashCode * 397) ^ Y;
				hashCode = (hashCode * 397) ^ Z;
				return hashCode;
			}
		}

		public CustomVector3(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public int X { get; }
		public int Y { get; }
		public int Z { get; }

		public static CustomVector3 operator +(CustomVector3 a, CustomVector3 b)
		{
			return new CustomVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static CustomVector3 operator -(CustomVector3 a, CustomVector3 b)
		{
			return new CustomVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static implicit operator CustomVector3((int x, int y, int z) source)
		{
			return new CustomVector3(source.x, source.y, source.z);
		}
	}
}