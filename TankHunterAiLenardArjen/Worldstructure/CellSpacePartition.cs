using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class CellSpacePartition // Chapter 3 pg 127
    {
        private List<int> neighbors;
        public List<BaseGameEntity> EntitiesInRange { get; }
        public Dictionary<int, Cell> Grid { get; }
        private double worldWidth;
        private double worldHeight;
        private int cellSize;
        private int numberOfCellsHeight;
        private int numberOfCellsWidth;
        private int totalNumberOfCells;

        public CellSpacePartition(double worldWidth, double worldHeight, int cellSize)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
            this.cellSize = cellSize;
            totalNumberOfCells = numberOfCellsHeight * numberOfCellsWidth;
            neighbors = new List<int>(totalNumberOfCells);
            EntitiesInRange = new List<BaseGameEntity>();
            GenerateGrid();
        }

        /// <summary>
        /// Creates a grid like this f.e (important for Calculate cell!)
        /// [0][3][6]
        /// [1][4][7]
        /// [2][5][8]      
        /// </summary>
        private void GenerateGrid()
        {
            int incrementId = 0;
            for (int i = 0; i < worldWidth; i += cellSize)
            {
                for (int j = 0; j < worldHeight; j += cellSize)
                {
                    Grid.Add(incrementId, new Cell(new Vector(i, j), incrementId));
                    incrementId++;
                }

                if (i == 0)
                {
                    numberOfCellsHeight = incrementId + 1;
                }
            }

            numberOfCellsWidth = (incrementId + 1) / numberOfCellsHeight;
        }

        //Add entity to cell based on cell id & add cell to entity
        public void AddEntity(BaseGameEntity entity, int id)
        {
            Cell value;
            Grid.TryGetValue(id, out value);
            value.Members.Add(entity);
            entity.InCell = value;
        }

        //Calculates cell id based on entity position
        private int CalculateCell(Vector position)
        {
            int cellValue = (int)(position.X * numberOfCellsHeight + position.Y);
            return cellValue;
        }

        //Updates Entity cell if needed
        public void UpdateEntity(MovingEntity entity) // basegame entities don't move so no update is needed
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

        /// <summary>
        /// NOT THE CASE ANYMORE
        ///     0_ _ _10-------X axis
        ///   0 [    T]
        ///   | [  *  ]  * = center of the cell/position e.g x= 10, y = 10
        ///   | [     ]  T = entity
        /// 10 Y axix        To check  which cells are within radius of T the max is calculated first because this can determine
        ///                  if its nessecary to caculate the the other ends.
        ///                  Note: Windows forms/monogame start the axis is the top left corner
        ///     
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="radius"></param>
        public void CalculateNeighbors(MovingEntity entity, int radius)
        {
            #region add cells to neighbor list
            int basicRadius = radius / cellSize;
            int rest = radius % cellSize;
            if (rest > 0) // slightly larger range than cell(s)
                basicRadius++;
            int tmp = entity.InCell.ID;
            int start = tmp - ((basicRadius * numberOfCellsHeight) - basicRadius);
            int finish = tmp + ((basicRadius * numberOfCellsHeight) + basicRadius);

            if (start < 0)
                start = 0;
            if (finish > totalNumberOfCells)
                finish = totalNumberOfCells - 1;

            while (start != finish)
            {
                //if the number is within range e.g radius is 2 
                //middle cell = 45
                // every cell within start and finish that 
                // mod number within radius -2 & + 2 so: 3,4,5,6,7
                if (start % numberOfCellsHeight <= rest + 2 && start % numberOfCellsHeight >= rest -2) 
                {
                    neighbors.Add(start);
                }
                start++;
            }
            #endregion
            EntitiesInRange.Clear();
            Cell oldValue;
            foreach (int id in neighbors)
            {
                Grid.TryGetValue(id, out oldValue);

                foreach(BaseGameEntity member in oldValue.Members)
                {
                    //TODO: Check correctness if statement
                    if(member.Position.X -  radius <= entity.Position.X  && member.Position.Y - radius <= entity.Position.Y || member.Position.X + radius >= entity.Position.X && member.Position.Y + radius >= entity.Position.Y)
                        EntitiesInRange.Add(member);
                }
            }
        }
    }
}
