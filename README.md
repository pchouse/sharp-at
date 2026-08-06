# SHARP AT

### This project is a c# suite of libraries for ERPs software to interact with the Portuguese Tax Authority and some utilities. 

#### This suite aims to be a set all libraries needed to the development of ERPs for Portuguese market.

#### To use clone the repository or download as a zip file and reference the projects that you need.

### Projects done 

 - AT QR code 
 - AT QR code REST API
 - Invoice calc 
 - Utilities

### Future Projects

 - SAF-T(PT)
 - AT Web services client to communicate invoices, working documents and payments documents
 - AT Web services client for communicate document serial
 - AT Web services to communicate stock movement documents
 - Hotel guest communication (SIBA)
 - Tax free

---

### Features

#### AT QR code and QR code Rest API
 - Build the qr code string from document
 - Generate the qr code image (png, jpg, svg) from the string or document 
 - Parse the qr code string to Document 
 - Parse the qr code image to string or document  

#### Utils
 - Client for the PDF Rest API https://github.com/pchouse/pt.pchouse.pdf . For generate PDFs with JasperReport, sign pdf with Multicert certificate account, convert PDF to image and print pdf with or without after print operations like cut paper or open cash drawer 
 - Entity Framework helper to ger scoped instance for parallel database connections 
 - Email helper to send email with attachment 
 - Helper to encrypt strings
 - Helper to configure JWT with roles 

#### Invoice calc
 - Uniformization of the invoice calculations in C# and TypeScript

--- 

## 🤝 Join the Project

This project is built on a simple idea: **ERP developers should spend their time building business value, not reinventing common infrastructure.**

Every ERP has its own business logic, workflows, and features that make it unique. However, many technical components are identical across projects: QR code generation, Tax Authority integrations, invoice calculations, PDF handling, email utilities, cryptography, authentication, and many other supporting libraries.

Instead of every company and every developer implementing and maintaining the same code repeatedly, we believe these common building blocks should be developed collaboratively as open-source libraries. This allows everyone to:

* Focus development effort on the core business logic of their ERP.
* Reduce development and maintenance costs.
* Improve software quality through community review and testing.
* Fix bugs and implement regulatory changes faster.
* Share knowledge and experience with other developers.

If you use this project, consider contributing back. Whether you report bugs, improve documentation, suggest new features, implement new modules, or review pull requests, every contribution helps make these libraries more reliable and useful for the entire ERP community.

Together, we can build a high-quality open-source ecosystem for ERP development in the Portuguese market, allowing developers to concentrate on what really matters: creating better software for their customers.


--- 
License MIT

Copyright 2023 Reflexão, Sistemas e Estudos Informáticos, Lda

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
THE USE OR OTHER DEALINGS IN THE SOFTWARE.

--- 
