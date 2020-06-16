# HumanFramework

## 简介

HumanFramework是一个以人体神经系统启发的一个以消息中心与IoC容器结合的解耦框架。

## 框架模块

### 消息中心

![](.\DocImg\SignalContainer.png)

### IoC注入

![IoC](.\DocImg\IoC.png)

### 对象池

![ObjPool](.\DocImg\ObjPool.png)

## 代码规范

```c#
//使用单行注释方式

//对public的成员变量使用Pascal写法
public int Health;

//对private的成员变量使用m+Pascal写法
private int mHealth;

//方法一律采用Pascal写法
//花括号需要换行对齐
public void OnDamage()
{
    
}

//接口名称加上字母I前缀
public interface IServiceProvider
```

