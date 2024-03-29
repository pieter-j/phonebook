import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Phonebook from './modules/phonebook/phonebook';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
      <Route path='/phonebook/:startIndex?' component={Phonebook} />
    </Layout>
);
