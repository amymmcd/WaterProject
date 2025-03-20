import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CookieConsent from 'react-cookie-consent';
import './App.css';
import ProjectList from './ProjectList';
import Fingerprint from './Fingerprint';
import Privacy from './Privacy';
import Navigation from './Navigation'; // Separate nav component

function App() {
  return (
    <Router>
      <Navigation />
      <Routes>
        <Route path="/projectList" element={<ProjectList />} />
        <Route path="/privacy" element={<Privacy />} />
      </Routes>
      <CookieConsent>
        This website uses cookies to enhance the user experience.
      </CookieConsent>
      <Fingerprint />
    </Router>
  );
}

export default App;
