# Testability

See unit and integration tests in _Tests_ folder.

# Extensibility

1. _CartService.DAL.ICartRepository_ interface may be used as an extension point when a need for another persistence technology arises (also it facilitates unit testing).
1. Purely theoretically, _CartService.BLL.ICartService_ may also be re-implemented differently (however, the only pragmatic utilization of this abstraction might be a proxy or testing mocking).