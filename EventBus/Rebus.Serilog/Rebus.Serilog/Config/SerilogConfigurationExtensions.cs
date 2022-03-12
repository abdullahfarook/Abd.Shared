﻿using System;
using Rebus.Serilog;
using Serilog;
using Serilog.Configuration;
// ReSharper disable UnusedMember.Global

namespace Rebus.Config;

/// <summary>
/// Configuration extensions for setting up logging with Serilog
/// </summary>
public static class SerilogConfigurationExtensions
{
    /// <summary>
    /// Configures Rebus to use Serilog for all of its internal logging, deriving its logger by pulling a base logger from the given <see cref="LoggerConfiguration"/>
    /// </summary>
    public static void Serilog(this RebusLoggingConfigurer configurer, LoggerConfiguration loggerConfiguration)
    {
        if (configurer == null) throw new ArgumentNullException(nameof(configurer));
        if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
        configurer.Use(new SerilogLoggerFactory(loggerConfiguration));
    }

    /// <summary>
    /// Configures Rebus to use Serilog for all of its internal logging, deriving its loggers from the given <see cref="ILogger"/> base logger
    /// </summary>
    public static void Serilog(this RebusLoggingConfigurer configurer, ILogger baseLogger)
    {
        if (configurer == null) throw new ArgumentNullException(nameof(configurer));
        if (baseLogger == null) throw new ArgumentNullException(nameof(baseLogger));
        configurer.Use(new SerilogLoggerFactory(baseLogger));
    }

    /// <summary>
    /// Configures Rebus to use Serilog for all of its internal logging, using Serilog's global <see cref="Log.Logger"/> as base logger
    /// </summary>
    public static void Serilog(this RebusLoggingConfigurer configurer)
    {
        if (configurer == null) throw new ArgumentNullException(nameof(configurer));
        configurer.Use(new SerilogLoggerFactory(Log.Logger));
    }

    /// <summary>
    /// Configures Serilog to add the correlation ID of the Rebus message currently being handled to log events as the <paramref name="propertyName"/>
    /// field. Does nothing when called outside of a message handler.
    /// </summary>
    public static LoggerConfiguration WithRebusCorrelationId(this LoggerEnrichmentConfiguration configuration, string propertyName)
    {
        return configuration.With(new RebusCorrelationIdEnricher(propertyName));
    }
}