using HahnCargoSim.Models.Grid;

namespace HahnCargoSimAutomation.Services;

public class SimulationBackgroundService : BackgroundService
{
    private List<Tuple<int, List<int>>> CargoTransporters { get; set; }
    private Grid Grid { get; set; }

    public SimulationBackgroundService()
    {
        CargoTransporters = [];
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Setup();

        Increment();
    }

    private void Increment()
    {

    }

    private void Setup()
    {
        //Retrieve grid
        if (Grid is not null)
            return;

        Grid = GetGrid();

        //Retrieve available orders
        var orders = GetAllOrdersAvailable();

        //Sort all orders with the shortest path with minimal cost then biggest value and then by deliveryDate
        var shortestOrders = OrderAllOrdersByPath(orders, Grid);

        //Hire first cargo transporter
        if (!CargoTransporters.Any())
        {
            //use the node from first item from listed order to hire the transporter at that node
            var cargoTransporter = HireCargoTrasporter(shortestOrders.First().OriginNodeId);
            CargoTransporters.Add(cargoTransporter);

            //Accept the best order to start
            AcceptBestOrder(shortestOrders.First().Id);
        }

        //check if the cargo transporter have the order assigned
        if (IsCargoTransporterLoaded(CargoTransporters.First(x => x.Item2.Contains(shortestOrders.First().Id)).Item1))
        {
            //Start to move the transporter to destination
            DoCargoTransporterMove();
        }
    }

    private Grid GetGrid()
    {
        throw new NotImplementedException();
    }

    private List<Order> GetAllOrdersAvailable()
    {
        throw new NotImplementedException();
    }

    private List<CalculatedOrder> OrderAllOrdersByPath(List<Order> orders, Grid grid)
    {
        throw new NotImplementedException();
    }

    private Tuple<int, List<int>> HireCargoTrasporter(object originNodeId)
    {
        throw new NotImplementedException();
    }

    private void AcceptBestOrder(object id)
    {
        throw new NotImplementedException();
    }

    private bool IsCargoTransporterLoaded(int item1)
    {
        throw new NotImplementedException();
    }
}
