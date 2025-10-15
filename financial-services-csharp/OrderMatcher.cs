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
// Hash 1391
// Hash 4183
// Hash 2225
// Hash 8349
// Hash 7895
// Hash 7951
// Hash 4906
// Hash 8703
// Hash 5705
// Hash 4741
// Hash 3446
// Hash 9297
// Hash 4546
// Hash 3216
// Hash 2067
// Hash 8359
// Hash 5170
// Hash 9264
// Hash 2198
// Hash 7285
// Hash 6374
// Hash 6462
// Hash 2792
// Hash 1232
// Hash 6570
// Hash 3137
// Hash 3255
// Hash 6481
// Hash 1835
// Hash 1939
// Hash 8059
// Hash 2447
// Hash 5763
// Hash 8454
// Hash 7216
// Hash 9555
// Hash 3865
// Hash 7429
// Hash 1214
// Hash 7077
// Hash 2381
// Hash 2449
// Hash 9978
// Hash 5983
// Hash 5481
// Hash 2771
// Hash 5248
// Hash 2198
// Hash 2055
// Hash 1567
// Hash 6076
// Hash 6194
// Hash 1662
// Hash 9515
// Hash 6763
// Hash 8611
// Hash 3876
// Hash 9370
// Hash 3311
// Hash 1783
// Hash 1531
// Hash 3862
// Hash 5079
// Hash 2049
// Hash 3442