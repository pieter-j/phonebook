import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Phonebook App Assessment</h1>
    <p>Welcome to your new single-page application, built with:</p>
    <ul>
      <li><a href='https://get.asp.net/'>ASP.NET Core 3.1</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
      <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
      <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
      <li>Entity framework Core 3.1 for ORM to interact with the SQL DB</li>
      <li>SQL server for DB</li>
    </ul>
    <p>A Few notes:</p>
    <ul>
      <li>Click on the Phonebook menu at the top of the screen to view the app</li>
      <li>For ease of development I use the spa integration in ASP.net core with React.  This means that in development mode the front end is using the React Dev server (node.js and Webpack) to enable hot loading.<br/>
         in a normal build asp.net (typically kestral, but can be configured to run behind iis or with different web listnners ) will serve the prebuild react files as static files.<br/>
         Because it is static files the front end can easily be hosted by a static file server e.g Amazon S3 or Azure storage containers with a CDN in front of it.  The api's urls is the only thing that need to change to enable this.
      </li>
      <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
    
      <li><p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p></li>
      <li>The db connection string in the appsettings file for dev is: "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Phonebook;Trusted_Connection=True;MultipleActiveResultSets=true" <br />
         which uses local DB and integrated security, so no secret information is checked into the repo.<br />
         To run this with a normal SQL server create a sql db and change the connection string in the apsettings file.<br/>
         In azure, you can set the DefaultConnection in the connection string settings for the website and it will override the info in the appsettings, again allowing production connections to be different without adding it to source controll.<br/>
      </li>
      <li>The server is set up to run the entity migrations on startup for ease of development and demos.  In production this one line should be removed, and the EF migration tool can be used to create static SQL scripts thand can be run to upgrade DB changes by a DB administrator.</li>
    </ul>
  </div>
);

export default connect()(Home);
