import { Link } from "react-router-dom";

function Navigation() {
  return (
    <nav>
      <h3><Link to="/projectList">Projects</Link></h3>
      <h3><Link to="/privacy">Privacy</Link></h3>
    </nav>
  );
}

export default Navigation;
