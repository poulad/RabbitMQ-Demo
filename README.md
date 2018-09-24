# RabbitMQ 🐰📫 Demo

🐰|🐰|🐰
:-:|:-:|:-:
| ️✉️ --> | 📪 |
| ️| 📫 |
| | 📬 | --> 📮
| ️| 📭 |

## Getting Started

1. Ensure .NET Core v2.1+ is installed
1. Start a RabbitMQ broker
    ```sh
    docker run --detach --publish 5672:5672 --publish 15672:15672 --name mq rabbitmq:3-management
    ```
1. Run the project in Development mode
    ```sh
    cd RabbitMQ-Demo
    dotnet run
    ```
1. Hit <http://localhost:5000/Queue/Push>
