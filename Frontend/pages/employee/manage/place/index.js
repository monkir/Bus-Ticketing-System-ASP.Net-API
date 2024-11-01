import { useEffect, useState } from "react"
import EmployeeHeader from "@/pages/employee/component/header"
import axios from "axios"
import Link from "next/link"
import EmployeeFooter from "../../component/footer"

export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')
  const [deleteId, setDeleteId] = useState()

  async function fetchData(searchValue = "") {
    try {
      // if (searchValue != "") {
      //   const response = await axios.get(
      //     process.env.NEXT_PUBLIC_api_root + '/api/employee/place/search/' + searchValue,
      //     {
      //       headers: { 'Authorization': sessionStorage.getItem('token_string') }
      //     }
      //   )
      //   setData(response.data)
      // }
      // else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/employee/place/all',
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      // }
    }
    catch (e) {
      try {
        console.log(e)
        setMessage(e.response.data.Message)
      }
      catch {
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  async function deleteData() {
    try {
      const response = await axios.delete(
        process.env.NEXT_PUBLIC_api_root + '/api/employee/notice/delete/' + deleteId,
        {
          headers: { 'Authorization': sessionStorage.getItem('token_string') }
        }
      )
      setMessage(response.data.message);
      fetchData();
    }
    catch (e) {
      try {
        console.log(e)
        setMessage(e.response.data.Message)
      }
      catch {
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  useEffect(() => {
    fetchData();
  }, [])

  async function search(event) {
    // console.log(event?.target?.value)
    event.preventDefault()
    const searchValue = event?.target?.value
    fetchData(searchValue)
  }
  return (
    <>
      <EmployeeHeader title="Bus Ticketing System" pagename="Employee: Manage Place"></EmployeeHeader>
      <div className="overflow-x-auto px-10 min-h-[70vh]">
        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            <Link className="btn btn-active btn-outline w-1/4" href={'/employee/manage/place/add'}>Add New Place</Link>
            {/* Search Box */}
            <input
              type="text"
              name="search"
              className="block w-3/4 rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              placeholder="search"
              onChange={search}
            />
          </div>
        </div>
        <h1> {data.length == 0 ? "No data found" : ""} </h1>
        <p className="text-2xl text-red-600 text-center">{message}</p>
        <table className="table table-zebra">
          {/* head */}
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Created By</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.name}</td>
                <td>{item.emp_id}</td>
                <td>
                  <Link className=" btn btn-info mx-1" href={"/employee/manage/place/edit/" + item.id}>Edit</Link>
                  {/* <span className=" btn btn-warning mx-1" onClick={() => { setDeleteId(item.id); document.getElementById('my_modal_1').showModal(); }} >Delete</span> */}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <EmployeeFooter/>
      {/* Delete Modal */}
      <dialog id="my_modal_1" className="modal">

        <div className="modal-box">
          <h3 className="font-bold text-lg">Hello!</h3>
          <p className="py-4">Sure to delete notice of id {deleteId}</p>
          <div className="modal-action">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button onClick={() => { setDeleteId(undefined); setMessage("Notice is not deleted") }} className="btn btn-info mx-1">Cancel</button>
              <button onClick={() => { deleteData() }} className="btn btn-warning mx-1">Ok</button>
            </form>
          </div>
        </div>
      </dialog>
    </>

  )
}

