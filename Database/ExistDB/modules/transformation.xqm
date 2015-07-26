xquery version "3.0";
module namespace vw = "http://vokabular.ujc.cas.cz/ns/it-jakub/1.0/transformation";

import module namespace xslfo="http://exist-db.org/xquery/xslfo";

declare default collation "http://exist-db.org/collation?lang=CS-cz";
declare namespace tei = "http://www.tei-c.org/ns/1.0";
declare namespace nlp = "http://vokabular.ujc.cas.cz/ns/tei-nlp/1.0";

declare namespace util = "http://exist-db.org/xquery/util";
declare namespace exist = "http://exist.sourceforge.net/NS/exist";

declare function vw:transform-document($node-to-transform as node(), $output-format as xs:string, $xsl-path as xs:string) as item() {
	let $template := doc(escape-html-uri($xsl-path)) 
let $transformation := 
	if($output-format = "Xml") then
			$node-to-transform
	else if($output-format = "Html") then
		transform:stream-transform($node-to-transform, $template, ())
	else if($output-format = "Rtf") 
		then vw:transform-document-to-rtf($node-to-transform, $template)
		else if($output-format = "Pdf") 
		then vw:transform-document-to-pdf($node-to-transform, $template)
		else()

return ($transformation)

} ;

declare function vw:transform-document-to-rtf($xml-document as node()?, $xslt-template as item()) as item() {
	let $extension := "rtf"
	return vw:transform-document-by-xslfo($xml-document, $xslt-template, 
		(), (), $extension)
};

declare function vw:transform-document-to-rtf($xml-document as node()?, $xslt-template as item(),
		$xslt-parameters as element()?, $xslfo-parameters as element()?) as item() {
	let $extension := "rtf"
	return vw:transform-document-by-xslfo($xml-document, $xslt-template, 
		$xsl-parameters, $xslfo-parameters, $extension)
};

declare function vw:transform-document-to-pdf($xml-document as node()?, $xslt-template as item()) as item() {
	let $extension := "pdf"
	return vw:transform-document-by-xslfo($xml-document, $xslt-template, 
		(), (), $extension)

};

declare function vw:transform-document-to-pdf($xml-document as node()?, $xslt-template as item(),
$xslt-parameters as item()?, $xslfo-parameters as item()?) as item() {
	let $extension := "pdf"
	return vw:transform-document-by-xslfo($xml-document, $xslt-template, 
		$xslt-parameters, $xslfo-parameters, $extension)
};

declare function vw:transform-document-by-xslfo($xml-document as node()?, $xslt-template as item(),
$xslt-parameters as item()?, $xslfo-parameters as item()?, $extension as xs:string) as item() {

	let $media-type as xs:string := "application/" || $extension
	let $filename := "fragment." ||  $extension

let $input-xml-fo-document := transform:transform($xml-document, $xslt-template, $xslt-parameters)

let $pdf-binary := response:stream-binary (
		xslfo:render($input-xml-fo-document, $media-type, $xslfo-parameters), 
		$media-type, $filename)
		let $pdf-binary := $input-xml-fo-document
	return $pdf-binary
}; 