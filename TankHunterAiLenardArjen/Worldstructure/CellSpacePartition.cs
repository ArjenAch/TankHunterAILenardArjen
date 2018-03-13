﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class CellSpacePartition // Chapter 3 pg 127
    {
        public List<Cell> Neighbors { get; }
        public List<BaseGameEntity> EntitiesInRange { get; }
        public Dictionary<int, Cell> Grid { get; }
        private double worldWidth;
        private double worldHeight;
        private int cellSize;
        private int numberOfCellsHeight;
        private int totalNumberOfCells;

        public CellSpacePartition(double worldWidth, double worldHeight, int cellSize)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
            this.cellSize = cellSize;
            Grid = new Dictionary<int, Cell>();
            Neighbors = new List<Cell>();
            EntitiesInRange = new List<BaseGameEntity>();
            GenerateGrid();
            GenerateEdges();
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
            for (int i = (cellSize / 2); i < worldWidth; i += cellSize)
            {
                for (int j = (cellSize / 2); j < worldHeight; j += cellSize)
                {
                    Cell cell = new Cell(new Vector(i, j), incrementId);
                    Grid.Add(incrementId, cell);
                    incrementId++;
                }

                if (i == (cellSize / 2))
                {
                    numberOfCellsHeight = incrementId;
                }
            }

            totalNumberOfCells = incrementId + 1;
        }

        private void GenerateEdges()
        {
            for (int i = 0; i < totalNumberOfCells - 1; i++)
            {
                CalculateNeighborCells(Grid[i], cellSize); // calculate direct neighbors

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
            Grid.TryGetValue(id, out value);
            value.Members.Add(entity);
            entity.InCell = value;
        }

        //Calculates cell id based on entity position
        private int CalculateCell(Vector position)
        {
            int cellDiv = (int)position.X / cellSize;
            int cellValue = (int)((cellDiv * numberOfCellsHeight) + (position.Y / cellSize));
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
                    if (j > worldHeight)
                    {
                        break;
                    }
                    tmp.X = i;
                    tmp.Y = j;
                    if (!(i < 0 || j < 0))
                    {
                        cellValue = CalculateCell(tmp);
                        if (!(cellValue < 0 || cellValue >= totalNumberOfCells - 1))
                        {
                            Neighbors.Add(Grid[cellValue]);
                        }
                    }
                }
                if (i > worldWidth)
                {
                    break;
                }
            }


            #region add cells to neighbor list
            //int basicRadius = radius / cellSize;
            //int rest = radius % cellSize;
            //if (rest > 0) // slightly larger range than cell(s)
            //    basicRadius++;

            //int tmp = entity.InCell.ID;
            //int start = tmp - ((basicRadius * numberOfCellsHeight) - basicRadius);
            //int finish = tmp + ((basicRadius * numberOfCellsHeight) + basicRadius);

            //if (start < 0)
            //    start = 0;
            //if (finish > totalNumberOfCells)
            //    finish = totalNumberOfCells - 1;

            //while (start != finish)
            //{
            //    //if the number is within range e.g radius is 2 
            //    //middle cell = 45
            //    // every cell within start and finish that 
            //    // mod number within radius -2 & + 2 so: 3,4,5,6,7
            //    if (start % numberOfCellsHeight <= rest + 2 && start % numberOfCellsHeight >= rest - 2)
            //    {
            //        neighbors.Add(start);
            //    }
            //    start++;
            //}
            #endregion
        }



        public void CalculateNeighborsEntities(MovingEntity entity, int radius)
        {
            CalculateNeighborCells(entity.InCell, radius);
            EntitiesInRange.Clear();

            foreach (Cell cell in Neighbors)
            {

                foreach (BaseGameEntity member in cell.Members)
                {
                    //TODO: Check correctness if statement
                    if (member.Position.X - radius <= entity.Position.X && member.Position.Y - radius <= entity.Position.Y || member.Position.X + radius >= entity.Position.X && member.Position.Y + radius >= entity.Position.Y)
                        EntitiesInRange.Add(member);
                }
            }
        }
    }
}