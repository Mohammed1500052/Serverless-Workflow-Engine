using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 9432
// Hash 2835
// Hash 9343
// Hash 7261
// Hash 4002
// Hash 3085
// Hash 1965
// Hash 9379
// Hash 7751
// Hash 6934
// Hash 5433
// Hash 4033
// Hash 9912
// Hash 4490
// Hash 9245
// Hash 1549
// Hash 8572
// Hash 9393
// Hash 2732
// Hash 7123
// Hash 6803
// Hash 2164
// Hash 9801
// Hash 4835
// Hash 9994
// Hash 7543
// Hash 5773
// Hash 3505
// Hash 2889
// Hash 3133
// Hash 2882
// Hash 1472
// Hash 7387
// Hash 6654
// Hash 9290
// Hash 5469
// Hash 6493
// Hash 1967
// Hash 1423
// Hash 7171
// Hash 4074
// Hash 2998
// Hash 4773
// Hash 4298
// Hash 3215
// Hash 5375
// Hash 7726
// Hash 3513
// Hash 5115
// Hash 4294
// Hash 2993
// Hash 8554