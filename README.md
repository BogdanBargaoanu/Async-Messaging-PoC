# Asyncronous Messaging Proof of Concept

A minimal implementation for **Asyncronous Messaging** featuring **NServiceBus**.
> Solution based on the guide provided by **ParticularSoftware** : [guide](https://docs.particular.net/tutorials/nservicebus-step-by-step/)


It features the following projects:
- `ClientUI` (Console App)
- `Sales` (Console App)
- `Sales.Messages` (Class Library)
- `Billing` (Console App)
- `Billing.Messages` (Class Library)
- `Shipping` (Console App)
---

## Workflow
The `ClientUI` endpoint will be sending a `PlaceOrder` command to the `Sales` endpoint. After receiving the command, the `Sales` endpoint will publish an `OrderPlaced` event, which will be received by the `Billing` and `Shipping` endpoints. After receiving an `OrderPlaced` event, the `Billing` endpoint will publish an `OrderBilled` event to the `Shipping` endpoint. An order is considered shipped when the `Shipping` endpoint, using a `Saga`, receives both an `OrderPlaced` and `OrderBilled` message.

![Diagram:](https://github.com/BogdanBargaoanu/Async-Messaging-PoC/blob/main/resources/diagram.svg)

## Running the Solution
#### 1. Clone the Repository
```bash
git clone https://github.com/BogdanBargaoanu/Async-Messaging-PoC.git
```

#### 2. Launch the Endpoints

Run the projects using the **Development** launch profile.

#### 3. Use the ClientUI to initiate an order.
> Press `P` for a new order, and `Q` to quit.
---

## Exception Retrying
For the demonstration purposes of the exception retrying feature of **NServiceBus**, the `PlaceOrderHandler` of the `Sales` endpoint throws an error 30% of the time. To modify the success rate, modify the following code in the `PlaceOrderHandler`:
```cs
 var random = new Random().Next();
 if (random > 0.7 * int.MaxValue)
 {
     // Throw error
 }
```
---
