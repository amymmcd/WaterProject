import { useEffect, useState } from 'react';
import { Project } from './types/Project';
import CookieConsent from "react-cookie-consent";


function ProjectList() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [pageSize, setPageSize] = useState<number>(10);
  const [pageNum, setPageNum] = useState<number>(1);
  const [totalItems, setTotalItems] = useState<number>(0);
  const [totalPages, setTotalPages] = useState<number>(0);

  useEffect(() => {
    const fetchProjects = async () => {
      const response = await fetch(
        `http://localhost:4000/api/Water/AllProjects?pageSize=${pageSize}&pageNum=${pageNum}`,
        {
          credentials: "include", //allows cookies
        }
      );
      const data = await response.json();
      setProjects(data.projects);
      setTotalItems(data.projectCount);
      setTotalPages(Math.ceil(data.projectCount / pageSize));
    };

    fetchProjects();
  }, [pageSize, pageNum]); // dependency array

  return (
    <>
      <h1>Water Projects</h1>
      <br />
      {projects.map((p) => (
        <div id="projectCard" className="card" key={p.projectId}>
          <h3 className="card-title, text-start">{p.projectName}</h3>
          <div className="card-body, text-start">
            <ul className="list-unstyled">
              <li>
                <strong>Project Type: </strong>
                {p.projectType}
              </li>
              <li>
                <strong>Regional Program: </strong>
                {p.projectRegionalProgram}
              </li>
              <li>
                <strong>Impact: </strong>
                {p.projectImpact}
              </li>
              <li>
                <strong>Phase: </strong>
                {p.projectPhase}
              </li>
              <li>
                <strong>Status: </strong>
                {p.projectFunctionalityStatus}
              </li>
            </ul>
          </div>
        </div>
      ))}

      <br />
      <button disabled={pageNum === 1} onClick={() => setPageNum(pageNum - 1)}>
        Previous
      </button>

      {[...Array(totalPages)].map((_, index) => (
        <button
          key={index + 1}
          onClick={() => setPageNum(index + 1)}
          disabled={pageNum === index + 1}
        >
          {index + 1}
        </button>
      ))}

      <button
        disabled={pageNum === totalPages}
        onClick={() => setPageNum(pageNum + 1)}
      >
        Next
      </button>

      <br />
      <label>
        Results per page:
        <select
          value={pageSize}
          onChange={(p) => {
            setPageSize(Number(p.target.value));
            setPageNum(1);
          }}
        >
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="20">20</option>
        </select>
      </label>
    </>
  );
}

export default ProjectList;
