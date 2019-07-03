using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Enitities;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class CellSpacePartition // Chapter 3 pg 127
    {
        public List<Cell> Neighbors { get; }
        public List<MovingEntity> EntitiesInRange { get; }
        public List<Obstacle> ObstaclesInRange { get; }
        public Dictionary<int, Cell> Grid { get; }
        public Texture2D DefaultTileTexture { get; set; }
        private int cellSize;
        public int NumberOfCellsHeight;
        public int TotalNumberOfCells;

        public CellSpacePartition( int cellSize)
        {
            this.cellSize = cellSize;
            Grid = new Dictionary<int, Cell>();
            Neighbors = new List<Cell>();
            EntitiesInRange = new List<MovingEntity>();
            ObstaclesInRange = new List<Obstacle>();
            GenerateGrid();
            GenerateEdges();
        }

        /// <summary>
        /// Creates a grid like this f.e: (important for Calculate cell!)
        /// [0][3][6]
        /// [1][4][7]
        /// [2][5][8]      
        /// TODO KNOWN BUG: outer cells rigth en downside get cells from opposite side as neighbors something todo with wrap around or calculate neighbors
        /// </summary>
        private void GenerateGrid()
        {
            int incrementId = 0;
            for (int i = (cellSize / 2); i <= (GlobalVars.worldWidth - (cellSize / 2)); i += cellSize)
            {
                for (int j = (cellSize / 2); j <= (GlobalVars.worldHeight - (cellSize / 2)); j += cellSize)
                {
                    Cell cell = new Cell(new Vector(i, j), incrementId);
                    Grid.Add(incrementId, cell);
                    incrementId++;
                }

                if (i == (cellSize / 2))
                {
                    NumberOfCellsHeight = incrementId;
                }
            }

            TotalNumberOfCells = incrementId + 1;
        }

        private void GenerateEdges()
        {
            for (int i = 0; i < TotalNumberOfCells - 1; i++)
            {
                CalculateNeighborCells(Grid[i], cellSize); // calculate direct neighbors

                Neighbors.Remove(Grid[i]);
                foreach (Cell cell in Neighbors)
                {

                    Grid[i].Adjecent.Add(new Edge(Grid[i], cell));
                }
            }
        }

        //Add entity to cell based on cell id & add cell to entity
        public void AddEntity(BaseGameEntity entity, int id)
        {
            Cell value;
            if (Grid.TryGetValue(id, out value))
            {
                if (entity is Obstacle)
                {
                    value.ContainstObstacle = true;
                    DeleteEdges(value);
                }

                value.Members.Add(entity);
                entity.InCell = value;
            }
        }

        private void DeleteEdges(Cell value)
        {
            foreach (Edge adjecent in value.Adjecent)
            {
                adjecent.Cell2.Adjecent.RemoveAll((x) => x.Cell2 == value);
            }
            value.Adjecent.Clear();
        }

        public Cell GetCellBasedOnPosition(Vector position)
        {
            int id = CalculateCell(position);
            return Grid[id];
        }



        //Calculates cell id based on entity position
        public int CalculateCell(Vector position)
        {
            position.WrapAround(GlobalVars.worldWidth, GlobalVars.worldHeight);
            if (EntityIsOutOfWorld(position))
            {
                int cellDiv = (int)position.X / cellSize;
                int cellValue = (int)((cellDiv * NumberOfCellsHeight) + (position.Y / cellSize));
                return cellValue;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Vector position is out of the playing field");
            }
        }

        private bool EntityIsOutOfWorld(Vector position) => !(position.X < 0 || position.X > GlobalVars.worldWidth || position.Y < 0 || position.Y > GlobalVars.worldHeight);

        //Updates Entity cell if needed
        public void UpdateEntity(BaseGameEntity entity)
        {
            try
            {
                int id = CalculateCell(entity.Position);

                if (entity.InCell.ID != id)
                {
                    Cell oldValue;
                    Grid.TryGetValue(entity.InCell.ID, out oldValue);
                    oldValue.Members.Remove(entity);
                    AddEntity(entity, id);
                }

            }
            catch (ArgumentOutOfRangeException) { }

        }

        public void CalculateNeighborCells(Cell center, int radius)
        {
            Neighbors.Clear();
            int basicRadius = radius / cellSize;
            int rest = radius % cellSize;
            if (rest > (cellSize / 2)) // slightly larger range than cell(s)
                basicRadius++;

            Vector currentVector = new Vector(center.Position.X - radius, center.Position.Y - radius);
            Vector finishVector = new Vector(center.Position.X + radius, center.Position.Y + radius);
            Vector tmp = new Vector(currentVector.X, currentVector.Y);
            int cellValue = -1;


            for (float i = currentVector.X; i <= finishVector.X; i += cellSize)
            {
                for (float j = currentVector.Y; j <= finishVector.Y; j += cellSize)
                {
                    if (j > GlobalVars.worldHeight)
                    {
                        break;
                    }
                    tmp.X = i;
                    tmp.Y = j;
                    if (!(i < 0 || j < 0))
                    {
                        try
                        {
                            // CalculateCell throws exception when a position is out side the game World
                            cellValue = CalculateCell(tmp);
                            if (!(cellValue < 0 || cellValue >= TotalNumberOfCells - 1))
                            {
                                Neighbors.Add(Grid[cellValue]);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                        }
                    }
                }
                if (i > GlobalVars.worldWidth)
                {
                    break;
                }
            }
        }

        public void CalculateNeighborsEntities(MovingEntity entity, int radius)
        {
            CalculateNeighborCells(entity.InCell, radius);
            EntitiesInRange.Clear();

            foreach (Cell cell in Neighbors)
            {
                foreach (BaseGameEntity member in cell.Members)
                {
                    if (member is MovingEntity && (member.Position.X - radius <= entity.Position.X && member.Position.Y - radius <= entity.Position.Y || member.Position.X + radius >= entity.Position.X && member.Position.Y + radius >= entity.Position.Y))
                        EntitiesInRange.Add((MovingEntity)member);
                }
            }
        }


        public void CalculateObstaclesWithinRadius(MovingEntity entity, int radius)
        {
            CalculateNeighborCells(entity.InCell, radius);
            ObstaclesInRange.Clear();

            foreach (Cell cell in Neighbors)
            {
                foreach (BaseGameEntity member in cell.Members)
                {
                    //TODO: Check correctness if statement
                    if (member is Obstacle && (member.Position.X - radius <= entity.Position.X && member.Position.Y - radius <= entity.Position.Y || member.Position.X + radius >= entity.Position.X && member.Position.Y + radius >= entity.Position.Y))
                    {
                        double range = radius + member.Bradius;
                        Vector to = member.Position - entity.Position;

                        //if entity within range, tag for further consideration.
                        if (to.LengthSq() < range * range)
                        {
                            ObstaclesInRange.Add((Obstacle)member);
                        }
                    }
                }
            }
        }

        //Shoud be called only once
        public void RenderAllCells(Texture2D texture, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TotalNumberOfCells - 1; i++)
            {
                Grid[i].Render(texture, spriteBatch);
            }
        }
    }
}
