using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    public partial class Form1 : Form
	{

		Map m_map;
		List<List<Label>> m_tiles;

		public Form1( )
        {
            InitializeComponent();
			m_map = new Map();
			m_tiles = new List<List<Label>>(m_map.SIZE);
			m_map.InitializeMap();
			InitializeTiles();
        }


		private void key_pressed( object sender, KeyEventArgs e )
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					m_map.UpdateMap(Keys.Up);
					break;
				case Keys.Down:
					m_map.UpdateMap(Keys.Down);
					break;
				case Keys.Left:
					m_map.UpdateMap(Keys.Left);
					break;
				case Keys.Right:
					m_map.UpdateMap(Keys.Right);
					break;
				default:
					return;
			}
			UpdateView();
		}

		private void UpdateView()
		{
			for ( int i = 0; i < m_tiles.Count; i++ )
				for ( int j = 0; j < m_tiles[i].Count; j++ )
					if ( m_map.Nodes[i][j] == CELL.EMPTY )
					{
						m_tiles[i][j].Text = "";
						m_tiles[i][j].BackColor = Color.Transparent;
					}
					else // change font size scaling for label
					{
						if ((int)m_map.Nodes[i][j] < 128)
							m_tiles[i][j].Font = new System.Drawing.Font("Showcard Gothic", 30,
											System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
											( (byte)( 0 ) ));
						
						else if ( (int)m_map.Nodes[i][j] > 512)
							m_tiles[i][j].Font = new System.Drawing.Font("Showcard Gothic", 16,
											System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
											( (byte)( 0 ) ));
						else if ((int)m_map.Nodes[i][j] > 64)
							m_tiles[i][j].Font = new System.Drawing.Font("Showcard Gothic", 24,
											System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
											( (byte)( 0 ) ));

						m_tiles[i][j].Text = ( (int)m_map.Nodes[i][j] ).ToString();
						m_tiles[i][j].BackColor = SetLabelColor(m_map.Nodes[i][j]);
					}
			scorebox.Text = m_map.Score.ToString();
		}
		


		private void InitializeTiles()
		{
			List<Label> temp = new List<Label>(m_map.SIZE);

			for (int i = 0; i < m_map.SIZE; i++)
			{
				m_tiles.Add(new List<Label>(m_map.SIZE));
				for (int j = 0; j < m_map.SIZE; j++)
					m_tiles[i].Add(new Label());
			}

			for ( int i = 0, yPos = 75; i < 4; i++)
				for ( int j = 0, xPos = 80; j < 4; j++ )
				{
					m_tiles[i][j].Font = new System.Drawing.Font("Showcard Gothic", 30,
											System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
											( (byte)( 0 ) ));
					//m_tiles[i][j].AutoSize = true;
					m_tiles[i][j].Location = new System.Drawing.Point(45 + xPos*j, 50 + yPos*i);
					m_tiles[i][j].Size = new System.Drawing.Size(75, 60);
					m_tiles[i][j].BorderStyle = BorderStyle.Fixed3D;
					m_tiles[i][j].TextAlign = ContentAlignment.MiddleCenter;
					if ( m_map.Nodes[i][j] == CELL.EMPTY )
						m_tiles[i][j].Text = "";
					else
						m_tiles[i][j].Text = ( (int)m_map.Nodes[i][j] ).ToString();
					m_tiles[i][j].BackColor = SetLabelColor(m_map.Nodes[i][j]);
					this.Controls.Add(m_tiles[i][j]);
				}
		}

		private Color SetLabelColor(CELL c)
		{
			switch(c)
			{
				case CELL.N1:
					return Color.AliceBlue;
				case CELL.N2:
					return Color.PapayaWhip;
				case CELL.N3:
					return Color.PeachPuff;
				case CELL.N4:
					return Color.Peru;
				case CELL.N5:
					return Color.Orange;
				case CELL.N6:
					return Color.OrangeRed;
				case CELL.N7:
					return Color.Red;
				case CELL.N8:
					return Color.Lime;
				case CELL.N9:
					return Color.Gold;
				case CELL.N10:
					return Color.Cyan;
				default:
					return Color.Transparent;
			}
			
		}

    }
}
