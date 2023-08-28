# Rabbitmq Connection Tester

Small C# application that allows you to check whether you can connect to Rabbit MQ

## Env Variables

| Name       | Description     | Value | required | default
|------------|-----------------| --- | ---- | --- |
| `host`     | RabbitMq host   | `""` | true | n/a |
| `username` | RabbitMq username | `""`  | false | "" |
| `password` | RabbitMq Password | `""` | false | "" |


## Kubernetes

I wrote this originally to deploy into a kubernetes namespace and see if my hosted RabbitMq version was working. 
Here is a yaml file so you can do the same


```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: rabbit-mq-connection-test
  namespace: my-namespace
spec:
  replicas: 1
  selector:
    matchLabels:
      app: rabbit-mq-connection-test
  template:
    metadata:
      labels:
        app: rabbit-mq-connection-test
    spec:
      containers:
        - name: rabbit-mq-connection-test
          image: "ghcr.io/freefoxsoftware/rabbit-mq-connection-tester:latest"
          imagePullPolicy: Always
          env:
            - name: host
              value: "my-rabbit-host"
            - name: username
              value: "user"
            - name: password
              value: "password"
```