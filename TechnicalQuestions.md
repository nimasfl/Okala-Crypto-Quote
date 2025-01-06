# Note

I’ve pushed my appsettings.json to make it easier for you to run the application. promise I’m not one of those people
who casually commit their environment to the git repo. 😉

# Technical Questions:

### 1. How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.

It took about 18 hours for me to completed this coding assignment.
If I had more time and this project was meant to be a larger one, I definitely would have implemented
MediatR package since it brings so much flexibility and also removes the need of injecting so many
services into each other just to have a function executing.

### 2. What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.

I Personally really enjoyed using Primary Constructors as it make the code really cleaner especially
when you need to inject multiple dependencies into a class. An example of it would be:

```csharp
public class ServiceA(IServiceB ServiceB, IServiceC ServiceC): IServiceA
{
    ...
}
```

instead of

```csharp
public class ServiceA: IServiceA
{
    private readonly ServiceB _serviceB;
    private readonly ServiceC _serviceC;
    
    public ServiceA(IServiceB serviceB, IServiceC serviceC) 
    {
        _serviceB = serviceB;
        _serviceC = serviceC;
    }
    
    ...
}
```

### 3. How would you track down a performance issue in production? Have you ever had to do this?

I Often use monitoring/logging tools to keep the track of issues happening on production. In Ahanonline company, where I
am working at the mean time, we use [Seq](https://datalust.co/seq) as Logging tool, [Sentry](https://sentry.io/welcome/)
for Monitoring services and applications and [Prometheus/Grafana](https://prometheus.io/) to have an overview of the
whole system all together.
The majority of performance issues I have encountered have been related to SQL queries. However, over the years of my
professional experience, I have dealt with a wide variety of challenges.

### 4. What was the latest technical book you have read or tech conference you have been to? What did you learn?

I recently read Clean Coder, a book that focuses on what it means to be a professional developer, emphasizing the human
aspects of the role beyond just writing code. Robert Martin shared numerous valuable insights from his extensive
experience, which I found highly beneficial.

### 5. What do you think about this technical assessment?

I appreciate the opportunity to take on the technical assessment, as it provided a practical way to demonstrate my
skills and approach to solving real-world problems. I found it well-structured and highly relevant to the role. In
particular, the user story and your expectations were clearly defined, which made it easier to focus on delivering a
solution aligned with your requirements.

### 6. Please, describe yourself using JSON.

```json
{
  "FirstName": "Nima",
  "LastName": "Soufiloo",
  "DateOfBirth": "1994-07-09T07:02:14.000Z",
  "Role": "Software Engineer",
  "UsedTechnologies": [
    "C#",
    "DotnetCore",
    "MSSQL",
    "Postgresql",
    "Git",
    "Docker",
    "NodeJs",
    "ReactJs",
    "VueJs",
    "Kafka",
    "Seq",
    "EfCore",
    "SignalR"
  ],
  "Hobbies": [
    "Gaming",
    "WatchingTV"
  ],
  "Characteristic": [
    "TeamPlayer",
    "Responsible",
    "HardWorker",
    "Respectful"
  ],
  "AboutMe": "I, Nima Soufiloo, am a software engineer. I started my work back in 2012. I started to develop applications in 2018. I am passionate about actively participating and communicating with product owners to help build rich product features. My enthusiasm for collaboration drives me to understand their vision and translate it into innovative and high-quality software solutions. I have successfully led groups of around 10 developers throughout these years, fostering a cohesive and productive team environment. I thrive in environments where I can contribute to the product development process, ensuring that each feature not only meets technical requirements but also aligns with the overall business goals. By maintaining open and effective communication, I aim to bridge the gap between technical implementation and product strategy, ultimately delivering exceptional user experiences."
}

```
