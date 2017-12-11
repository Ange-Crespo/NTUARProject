'use strict';
const createRouter = require('@arangodb/foxx/router');
const router = createRouter();

module.context.use(router);
var fs = require("fs");

router.get('/hello-world', function (req, res) 
{
  res.send('Hello World!');
}).response(['text/plain'], 'A generic greeting.').summary('Generic greeting').description('Prints a generic greeting.');


router.post('/receive-binary', function (req, res) {
  
    //var path = "/objet.obj";
    // fetch request body into a Buffer
    var body = req.rawBody;
    
    //var bodyBin= new Buffer(body , "binary");
    var path = req.queryParams.path;
    var id = req.queryParams.id;
    var type = req.queryParams.type;
    var bigPath = path+"/"+id+"."+type;
    
    if (fs.isDirectory(path))
    {
    }else
    {
      fs.makeDirectory(path);
    }
    
    fs.write(bigPath,body);
    //res.json(body);
    res.json(body);
  }).response(['text/plain'], 'A generic greeting.')
  .summary('Generic greeting')
  .description('Prints a generic greeting.');
  
  router.get('/provide-binary-file', function (req, res) 
  { 
    //var test = req.pathParams.id;
    // create an absolute filename, local to the Foxx application directory
    //var filename = module.context.fileName("cube.obj");
    // send the contents, this will also set mime type "application/octet-stream"
    //res.sendFile(filename);
    //console.log(req.body);
    //res.send('You have the file ! ' + req );
    //res.json(req.queryParams);
  
    //var path = req.pathParams.path;
  
    //res.sendFile(path);
    res.sendFile(decodeURIComponent(req.queryParams.path));
  }).response(['text/plain'], 'A generic greeting.')
  .summary('Generic greeting')
  .description('Prints a generic greeting.');
  
  /*app.use(function(req, res, next) {
    var data = new Buffer('');
    req.on('data', function(chunk) {
        data = Buffer.concat([data, chunk]);
    });
    req.on('end', function() {
      req.rawBody = data;
      next();
    });
  });*/