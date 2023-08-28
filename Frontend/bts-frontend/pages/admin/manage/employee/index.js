import { useEffect, useState } from "react"
import MyHeader from "./../../component/header"
import axios from "axios"

const links = [
  { name: 'Open roles', href: '#' },
  { name: 'Internship program', href: '#' },
  { name: 'Our values', href: '#' },
  { name: 'Meet our leadership', href: '#' },
]
const stats = [
  { name: 'Offices worldwide', value: '12' },
  { name: 'Full-time colleagues', value: '300+' },
  { name: 'Hours per week', value: '40' },
  { name: 'Paid time off', value: 'Unlimited' },
]

export default function Example() {
  const [data, setData] = useState([])
  useEffect(()=>{
    async function fetchData(){
      try{
        const response = await axios.get(
            'https://localhost:44304/api/admin/employee/all',
            {
                headers: {'Authorization': sessionStorage.getItem('token_string')}
            }
        )
        // console.log(response.data)
        setData(response.data)
      }
      catch(e){
        try{
          setInfo(e.response.data.Message)
        }catch{
          console.log(e)
        }
      }
    }
    fetchData();
  }, [])
  return (
    <>
    <MyHeader title="Bus Ticketing System" pagename="Admin Panel: Manage Employee"></MyHeader>
    <div className="overflow-x-auto px-10">
      <table className="table table-zebra">
        {/* head */}
        <thead>
          <tr>
            <th>ID</th>
            <th>username</th>
            <th>Name</th>
            <th>Salary</th>
            <th>DBO</th>
            <th>Created BY</th>
          </tr>
        </thead>
        <tbody>
          {data.map(item=>(
            <tr key={item.id}>
              <th>{item.id}</th>
              <td>{item.username}</td>
              <td>{item.name}</td>
              <td>{item.salary}</td>
              <td>{item.dob}</td>
              <td>{item.admin_id}</td>
            </tr>
          ))}
          
        </tbody>
      </table>
    </div>
    </>
    
  )
}

