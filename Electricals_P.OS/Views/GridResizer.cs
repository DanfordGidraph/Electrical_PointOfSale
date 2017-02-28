using System.Windows.Controls;


namespace Electricals_PointOfSale.Views
{
    public class GridResizer
    {
        public void shrinkGrids(Grid[] gridArray, Grid grid)
        {
            for (int i = 0; i < gridArray.Length; i++)
            {
                if (gridArray[i] != grid)
                {
                    gridArray[i].Height = 0;

                }

            }
            _ExpandGrid(grid);
        }
        private void _ExpandGrid(Grid grid)
        {
            grid.Height = 390;
        }



    }
}
