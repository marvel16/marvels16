using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Game2048
{


	public struct Point
	{
		public int x { get; set; }
		public int y { get; set; }
		public Point( int _x = 0, int _y = 0 ) : this()
			
		{
			x = _x;
			y = _y;
		}

	}


	public enum DirKey { UP = 0, DOWN, LEFT, RIGHT };

	public enum CELL { EMPTY = 0, N1 = 2, N2 = 4, N3 = 8, N4 = 16, N5 = 32, N6 = 64, N7 = 128, N8 = 256, N9 = 512, N10 = 1024 };

	public static class Cell
	{
		public static CELL Next( CELL val )
		{
			return (CELL)( ( (int)val << 1 ) );
		}
	}

	public class Map
	{
		public readonly int SIZE = 4;
		public List<List<CELL>> Nodes { get; set; }
		Random rand = new Random();
		public int Score { get; set; }
		public Map( )
		{
			Nodes = new List<List<CELL>>(SIZE);
			for (int i = 0; i < SIZE; i++)
			{
				Nodes.Add(new List<CELL>(SIZE));
				for (int j = 0; j < SIZE; j++)
					Nodes[i].Add(CELL.EMPTY);
			}
		}

		public void InitializeMap( )
		{
			for (int i = 0; i < SIZE; i++)
				for (int j = 0; j < SIZE; j++)
					Nodes[i][j] = CELL.EMPTY;
			SpawnNode();
			SpawnNode();
			Score = 0;
		}

		public void UpdateMap(Keys key)
		{
			List<List<CELL>> tempNodes = new List<List<CELL>>(Nodes.Count);
			for (int i = 0; i < Nodes.Count; i++) //copy nodes to compare if changed
				tempNodes.Add(new List<CELL>(Nodes[i]));
			switch ( key )
			{
				case Keys.Up:
					RotateBy90toRight();
					MoveNodes();
					RotateBy90toLeft();
					break;
				case Keys.Down:
					RotateBy90toLeft();
					MoveNodes();
					RotateBy90toRight();
					break;
				case Keys.Left:
					RotateBy180();
					MoveNodes();
					RotateBy180();
					break;
				case Keys.Right:
					MoveNodes();
					break;
			}

			if(MapChanged(ref tempNodes))
				SpawnNode();
			else
				if (!FindFreeCells())
				{
					MessageBox.Show("Game over with Score " + Score);
					InitializeMap();
				}
		}


		private void SpawnNode()
		{
			List<Point> freeNodes = new List<Point>();
			FindFreeCells(ref freeNodes);
			
			CELL generatedValue;
			if ( rand.Next(0, 10) == 0 )
				generatedValue = CELL.N2;
			else
				generatedValue = CELL.N1;
			int index = rand.Next(0, freeNodes.Count - 1);
			Nodes[freeNodes[index].y][freeNodes[index].x] = generatedValue;
		}

		private void MoveNodes( )
		{
			List<List<CELL>> tempNodes = new List<List<CELL>>(Nodes);
			MergeTiles();
			int emptyCount;
			for ( int j = 0; j < Nodes.Count; j++ )
			{
				emptyCount = 0;
				for ( int i = Nodes[j].Count - 2; i >= 0; i-- )
				{
					if ( Nodes[j][i + 1] == CELL.EMPTY )
					{
						if ( Nodes[j][i] != CELL.EMPTY )
							swap(new Point(i + 1 + emptyCount, j), new Point(i, j));
						else
							emptyCount++;
					}
				}
			}
		}

		private void MergeTiles( )
		{
			int next;
			for ( int j = 0; j < Nodes.Count; j++ )
				for ( int i = Nodes[j].Count - 1; i >= 1; i-- )
				{
					next = 1;
					if ( Nodes[j][i] != CELL.EMPTY )
					{
						while ( i - next > 0 && Nodes[j][i - next] == CELL.EMPTY )
							next++; //while next value found
						if ( Nodes[j][i - next] == CELL.EMPTY ) break;
						if ( Nodes[j][i - next] == Nodes[j][i] )
						{
							Nodes[j][i] = Cell.Next(Nodes[j][i - next]);
							Nodes[j][i-next] = CELL.EMPTY;
							Score +=  (int)Nodes[j][i];
							i -= next;
						}
					}
				}
		}


		public void ShowMap( )
		{
			Console.WriteLine();
			for ( int i = 0; i < Nodes.Count; i++ )
			{
				Console.WriteLine();
				for ( int j = 0; j < Nodes[i].Count; j++ )
					Console.Write("{0}   ", (int)Nodes[i][j]);
			}
			Console.WriteLine();
		}

		private void RotateBy90toLeft( )
		{
			Transpose();
			SwapRows();
		}

		private void RotateBy90toRight( )
		{
			SwapRows();
			Transpose();
		}

		private void RotateBy180()
		{
			RotateBy90toLeft();
			RotateBy90toLeft();
		}

		private void Transpose( )
		{
			CELL temp;
			for ( int j = 0; j < Nodes.Count - 1; j++ )
				for ( int i = j + 1; i < Nodes[j].Count; i++ )
				{
					temp = Nodes[j][i];
					Nodes[j][i] = Nodes[i][j];
					Nodes[i][j] = temp;
				}
		}

		private void SwapRows( )
		{
			for ( int i = 0, k = Nodes.Count-1; i < k; ++i, --k )
			{
				List<CELL> x = Nodes[i];
				Nodes[i] = Nodes[k];
				Nodes[k] = x;
			}
		}

		private bool FindFreeCells( ref List<Point> freeNodes )
		{
			for ( int i = 0; i < Nodes.Count; i++ )
				for ( int j = 0; j < Nodes[i].Count; j++ )
					if ( Nodes[i][j] == CELL.EMPTY )
						freeNodes.Add(new Point(j, i));
			return freeNodes.Count > 0 ? true : false;
		}

		private bool FindFreeCells()
		{
			for ( int i = 0; i < Nodes.Count; i++ )
				for ( int j = 0; j < Nodes[i].Count; j++ )
					if (Nodes[i][j] == CELL.EMPTY)
						return true;
			return false;
		}

		private void swap( Point p1, Point p2 )
		{
			CELL temp = Nodes[p1.y][p1.x];
			Nodes[p1.y][p1.x] = Nodes[p2.y][p2.x];
			Nodes[p2.y][p2.x] = temp;
		}

		private bool MapChanged(ref List<List<CELL>> tempNodes)
		{
			for (int i = 0; i < tempNodes.Count; i++)
				for (int j = 0; j < tempNodes[i].Count; j++)
					if (tempNodes[i][j] != Nodes[i][j])
						return true;
			return false;
		}

	}

	



}