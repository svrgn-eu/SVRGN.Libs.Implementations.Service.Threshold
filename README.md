[TOC]

# About

This Service aims to provide services for data classification. Within a number range, you are able to define several thresholds which then can be checked against a value. This can be done by instantiating and adding several IThreshold implementations to the IThreshold implementation.

# How to use

After instantiating a IThreshold Service instance (DI preferred) you basically create as many IThreshold instances as necessary. Those are being handled by the DI in the attached tests.

It is recommended to have a transient service as different scenarios require different thresholds.

You then can add the thresholds, a default text and the check any value by calling "GetText" - if a threshold is found, you will return its' text, otherwise the default text will be returned.

```csharp
	IThreshold threshold1 = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");
	IThreshold threshold2 = this.objectService.Create<IThreshold>(0.11f, 0.2f, "Ping2");

	thresholdService.Add(threshold1);
	thresholdService.Add(threshold2);

	string newDefaultText = "default";
	thresholdService.SetDefaultText(newDefaultText);

	string defaultText = thresholdService.DefaultText;
	string valueText = thresholdService.GetText(0.05f);
```

Be sure to check the test project for more examples!