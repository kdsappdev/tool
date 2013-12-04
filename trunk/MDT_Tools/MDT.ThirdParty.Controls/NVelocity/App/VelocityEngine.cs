using System;
using System.IO;

using Template = NVelocity.Template;
using InternalContextAdapterImpl = NVelocity.Context.InternalContextAdapterImpl;
using RuntimeSingleton = NVelocity.Runtime.RuntimeSingleton;
using SimpleNode = NVelocity.Runtime.Parser.Node.SimpleNode;
using VelocimacroProxy = NVelocity.Runtime.Directive.VelocimacroProxy;
using ResourceNotFoundException = NVelocity.Exception.ResourceNotFoundException;
using ParseErrorException = NVelocity.Exception.ParseErrorException;
using MethodInvocationException = NVelocity.Exception.MethodInvocationException;
using ParseException = NVelocity.Runtime.Parser.ParseException;
using Commons.Collections;
using NVelocity.Context;
using NVelocity.Runtime;


namespace NVelocity.App {
	
    /// <summary>
    /// <p>
    /// This class provides a separate new-able instance of the
    /// Velocity template engine.  The alternative model for use
    /// is using the Velocity class which employs the singleton
    /// model.
    /// </p>
    /// <p>
    /// Please ensure that you call one of the init() variants. 
    /// This is critical for proper behavior.  
    /// </p>
    /// <p> Coming soon : Velocity will call
    /// the parameter-less init() at the first use of this class
    /// if the init() wasn't explicitly called.  While this will
    /// ensure that Velocity functions, it almost certainly won't
    /// function in the way you intend, so please make sure to
    /// call init().
    /// </p>
    /// </summary>
    /// <author> <a href="mailto:geirm@optonline.net">Geir Magnusson Jr.</a></author>
    public class VelocityEngine : RuntimeConstants {

	private RuntimeInstance ri = new RuntimeInstance();

	public VelocityEngine() {
	}

	/// <summary>
	/// Set an entire configuration at once. This is
	/// useful in cases where the parent application uses
	/// the ExtendedProperties class and the velocity configuration
	/// is a subset of the parent application's configuration.
	/// </summary>
	/// <param name="ExtendedProperties">configuration
	/// </param>
	public virtual ExtendedProperties ExtendedProperties {
	    set { ri.Configuration = value; }
	}
		
	/// <summary>
	/// initialize the Velocity runtime engine, using the default 
	/// properties of the Velocity distribution
	/// </summary>
	public virtual void Init() {
	    ri.init();
	}
		
	/// <summary>
	/// initialize the Velocity runtime engine, using default properties
	/// plus the properties in the properties file passed in as the arg
	/// </summary>
	/// <param name="propsFilename">file containing properties to use to initialize 
	/// the Velocity runtime</param>
	public virtual void Init(System.String propsFilename) {
	    ri.init(propsFilename);
	}
		
	/// <summary>  
	/// initialize the Velocity runtime engine, using default properties
	/// plus the properties in the passed in java.util.Properties object
	/// </summary>
	/// <param name="p"> Proprties object containing initialization properties</param>
	public virtual void Init(ExtendedProperties p) {
	    ri.init(p);
	}
		
	/// <summary> 
	/// Set a Velocity Runtime property.
	/// </summary>
	/// <param name="String">key</param>
	/// <param name="Object">value</param>
	public virtual void SetProperty(System.String key, System.Object value_Renamed) {
	    ri.setProperty(key, value_Renamed);
	}
		
	/// <summary> 
	/// Add a Velocity Runtime property.
	/// </summary>
	/// <param name="String">key
	/// </param>
	/// <param name="Object">value
	/// </param>
	public virtual void AddProperty(System.String key, System.Object value_Renamed) {
	    ri.addProperty(key, value_Renamed);
	}
		
	/// <summary> 
	/// Clear a Velocity Runtime property.
	/// </summary>
	/// <param name="key">of property to clear
	/// </param>
	public virtual void ClearProperty(System.String key) {
	    ri.clearProperty(key);
	}
		
	/// <summary>
	/// Get a Velocity Runtime property.
	/// </summary>
	/// <param name="key">property to retrieve
	/// </param>
	/// <returns>property value or null if the property
	/// not currently set
	/// </returns>
	public virtual System.Object GetProperty(System.String key) {
	    return ri.getProperty(key);
	}
		
	/// <summary>
	/// renders the input string using the context into the output writer. 
	/// To be used when a template is dynamically constructed, or want to use 
	/// Velocity as a token replacer.
	/// </summary>
	/// <param name="context">context to use in rendering input string
	/// </param>
	/// <param name="out"> Writer in which to render the output
	/// </param>
	/// <param name="logTag"> string to be used as the template name for log 
	/// messages in case of error
	/// </param>
	/// <param name="instring">input string containing the VTL to be rendered
	/// </param>
	/// <returns>true if successful, false otherwise.  If false, see
	/// Velocity runtime log
	/// </returns>
	public virtual bool Evaluate(IContext context, TextWriter out_Renamed, System.String logTag, System.String instring) {
	    return Evaluate(context, out_Renamed, logTag, new StringReader(instring));
	}
		
	/// <summary> 
	/// Renders the input stream using the context into the output writer.
	/// To be used when a template is dynamically constructed, or want to
	/// use Velocity as a token replacer.
	/// </summary>
	/// <param name="context">context to use in rendering input string
	/// </param>
	/// <param name="out"> Writer in which to render the output
	/// </param>
	/// <param name="logTag"> string to be used as the template name for log messages
	/// in case of error
	/// </param>
	/// <param name="instream">input stream containing the VTL to be rendered
	/// </param>
	/// <returns>true if successful, false otherwise.  If false, see 
	/// Velocity runtime log
	/// </returns>
	/// <deprecated>Use
	/// {@link #evaluate( Context context, Writer writer, 
	/// String logTag, Reader reader ) }
	/// </deprecated>
	public virtual bool Evaluate(IContext context, TextWriter writer, System.String logTag, Stream instream) {
	    /*
	    *  first, parse - convert ParseException if thrown
	    */
	    TextReader br = null;
	    System.String encoding = null;
			
	    try {
		encoding = ri.getString(RuntimeConstants_Fields.INPUT_ENCODING, RuntimeConstants_Fields.ENCODING_DEFAULT);
		br = new StreamReader(new StreamReader(instream, System.Text.Encoding.GetEncoding(encoding)).BaseStream);
	    } catch (IOException uce) {
		System.String msg = "Unsupported input encoding : " + encoding + " for template " + logTag;
		throw new ParseErrorException(msg);
	    }
			
	    return Evaluate(context, writer, logTag, br);
	}
		
	/// <summary>
	/// Renders the input reader using the context into the output writer.
	/// To be used when a template is dynamically constructed, or want to
	/// use Velocity as a token replacer.
	/// </summary>
	/// <param name="context">context to use in rendering input string
	/// </param>
	/// <param name="out"> Writer in which to render the output
	/// </param>
	/// <param name="logTag"> string to be used as the template name for log messages
	/// in case of error
	/// </param>
	/// <param name="reader">Reader containing the VTL to be rendered
	/// </param>
	/// <returns>true if successful, false otherwise.  If false, see 
	/// Velocity runtime log
	/// @since Velocity v1.1
	/// </returns>
	public virtual bool Evaluate(IContext context, TextWriter writer, System.String logTag, TextReader reader) {
	    SimpleNode nodeTree = null;
			
	    try {
		nodeTree = ri.parse(reader, logTag);
	    } catch (ParseException pex) {
		throw new ParseErrorException(pex.Message);
	    }
			
	    /*
	    * now we want to init and render
	    */
	    if (nodeTree != null) {
		InternalContextAdapterImpl ica = new InternalContextAdapterImpl(context);
				
		ica.PushCurrentTemplateName(logTag);
				
		try {
		    try {
			nodeTree.init(ica, ri);
		    }
		    catch (System.Exception e) {
			ri.error("Velocity.evaluate() : init exception for tag = " + logTag + " : " + e);
		    }
					
		    /*
		    *  now render, and let any exceptions fly
		    */
					
		    nodeTree.render(ica, writer);
		} finally {
		    ica.PopCurrentTemplateName();
		}
				
		return true;
	    }
			
	    return false;
	}
		
		
	/// <summary>
	/// Invokes a currently registered Velocimacro with the parms provided
	/// and places the rendered stream into the writer.
	/// Note : currently only accepts args to the VM if they are in the context. 
	/// </summary>
	/// <param name="vmName">name of Velocimacro to call
	/// </param>
	/// <param name="logTag">string to be used for template name in case of error
	/// </param>
	/// <param name="params[]">args used to invoke Velocimacro. In context key format : 
	/// eg  "foo","bar" (rather than "$foo","$bar")
	/// </param>
	/// <param name="context">Context object containing data/objects used for rendering.
	/// </param>
	/// <param name="writer"> Writer for output stream
	/// </param>
	/// <returns>true if Velocimacro exists and successfully invoked, false otherwise.
	/// </returns>
	public virtual bool InvokeVelocimacro(System.String vmName, System.String logTag, System.String[] params_Renamed, IContext context, TextWriter writer) {
	    /*
	    *  check parms
	    */
	    if (vmName == null || params_Renamed == null || context == null || writer == null || logTag == null) {
		ri.error("VelocityEngine.invokeVelocimacro() : invalid parameter");
		return false;
	    }
			
	    /*
	    * does the VM exist?
	    */
	    if (!ri.isVelocimacro(vmName, logTag)) {
		ri.error("VelocityEngine.invokeVelocimacro() : VM '" + vmName + "' not registered.");
		return false;
	    }
			
	    /*
	    *  now just create the VM call, and use evaluate
	    */
	    System.Text.StringBuilder construct = new System.Text.StringBuilder("#");
			
	    construct.Append(vmName);
	    construct.Append("(");
			
	    for (int i = 0; i < params_Renamed.Length; i++) {
		construct.Append(" $");
		construct.Append(params_Renamed[i]);
	    }
			
	    construct.Append(" )");
			
	    try {
		bool retval = Evaluate(context, writer, logTag, construct.ToString());
				
		return retval;
	    }
	    catch (System.Exception e) {
		ri.error("VelocityEngine.invokeVelocimacro() : error " + e);
		throw e;
	    }
	}
		
	/// <summary>
	/// merges a template and puts the rendered stream into the writer
	/// </summary>
	/// <param name="templateName">name of template to be used in merge
	/// </param>
	/// <param name="context"> filled context to be used in merge
	/// </param>
	/// <param name="writer"> writer to write template into
	/// </param>
	/// <returns>true if successful, false otherwise.  Errors 
	/// logged to velocity log.
	/// </returns>
	/// <deprecated>Use
	/// {@link #mergeTemplate( String templateName, String encoding,
	/// Context context, Writer writer )}
	/// </deprecated>
	public virtual bool MergeTemplate(System.String templateName, IContext context, TextWriter writer) {
	    return MergeTemplate(templateName, ri.getString(RuntimeConstants_Fields.INPUT_ENCODING, RuntimeConstants_Fields.ENCODING_DEFAULT), context, writer);
	}
		
	/// <summary>
	/// merges a template and puts the rendered stream into the writer
	/// </summary>
	/// <param name="templateName">name of template to be used in merge
	/// </param>
	/// <param name="encoding">encoding used in template
	/// </param>
	/// <param name="context"> filled context to be used in merge
	/// </param>
	/// <param name="writer"> writer to write template into
	/// </param>
	/// <returns>true if successful, false otherwise.  Errors 
	/// logged to velocity log
	/// @since Velocity v1.1
	/// </returns>
	public virtual bool MergeTemplate(System.String templateName, System.String encoding, IContext context, TextWriter writer) {
	    Template template = ri.getTemplate(templateName, encoding);
			
	    if (template == null) {
		ri.error("Velocity.parseTemplate() failed loading template '" + templateName + "'");
		return false;
	    }
	    else {
		template.Merge(context, writer);
		return true;
	    }
	}
		
	/// <summary>
	/// Returns a <code>Template</code> from the Velocity
	/// resource management system.
	/// </summary>
	/// <param name="name">The file name of the desired template.
	/// </param>
	/// <returns>    The template.
	/// @throws ResourceNotFoundException if template not found
	/// from any available source.
	/// @throws ParseErrorException if template cannot be parsed due
	/// to syntax (or other) error.
	/// @throws Exception if an error occurs in template initialization
	/// </returns>
	public virtual Template GetTemplate(System.String name) {
	    return ri.getTemplate(name);
	}
		
	/// <summary>
	/// Returns a <code>Template</code> from the Velocity
	/// resource management system.
	/// </summary>
	/// <param name="name">The file name of the desired template.
	/// </param>
	/// <param name="encoding">The character encoding to use for the template.
	/// </param>
	/// <returns>    The template.
	/// @throws ResourceNotFoundException if template not found
	/// from any available source.
	/// @throws ParseErrorException if template cannot be parsed due
	/// to syntax (or other) error.
	/// @throws Exception if an error occurs in template initialization
	/// @since Velocity v1.1
	/// </returns>
	public virtual Template GetTemplate(System.String name, System.String encoding) {
	    return ri.getTemplate(name, encoding);
	}
		
	/// <summary>
	/// Determines if a template is accessable via the currently 
	/// configured resource loaders.
	/// <br><br>
	/// Note that the current implementation will <b>not</b>
	/// change the state of the system in any real way - so this
	/// cannot be used to pre-load the resource cache, as the 
	/// previous implementation did as a side-effect. 
	/// <br><br>
	/// The previous implementation exhibited extreme lazyness and
	/// sloth, and the author has been flogged.
	/// </summary>
	/// <param name="templateName"> name of the temlpate to search for
	/// </param>
	/// <returns>true if found, false otherwise
	/// </returns>
	public virtual bool TemplateExists(System.String templateName) {
	    return (ri.getLoaderNameForResource(templateName) != null);
	}
		
	/// <summary>
	/// Log a warning message.
	/// </summary>
	/// <param name="Object">message to log</param>
	public virtual void Warn(System.Object message) {
	    ri.warn(message);
	}
		
	/// 
	/// <summary>
	/// Log an info message.
	/// </summary>
	/// <param name="Object">message to log</param>
	public virtual void Info(System.Object message) {
	    ri.info(message);
	}
		
	/// <summary>
	/// Log an error message.
	/// </summary>
	/// <param name="Object">message to log</param>
	public virtual void Error(System.Object message) {
	    ri.error(message);
	}
		
	/// <summary>
	/// Log a debug message.
	/// </summary>
	/// <param name="Object">message to log</param>
	public virtual void Debug(System.Object message) {
	    ri.debug(message);
	}
		
	/// <summary>
	/// <p>
	/// Set the an ApplicationAttribue, which is an Object
	/// set by the application which is accessable from
	/// any component of the system that gets a RuntimeServices.
	/// This allows communication between the application
	/// environment and custom pluggable components of the
	/// Velocity engine, such as loaders and loggers.
	/// </p>
	/// <p>
	/// Note that there is no enfocement or rules for the key
	/// used - it is up to the application developer.  However, to
	/// help make the intermixing of components possible, using
	/// the target Class name (e.g.  com.foo.bar ) as the key
	/// might help avoid collision.
	/// </p>
	/// </summary>
	/// <param name="key">object 'name' under which the object is stored</param>
	/// <param name="value">object to store under this key</param>
	public virtual void SetApplicationAttribute(System.Object key, System.Object value_Renamed) {
	    ri.setApplicationAttribute(key, value_Renamed);
	}

    }
}