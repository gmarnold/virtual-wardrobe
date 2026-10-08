import { apiBaseUrl } from './config'

// Keep aligned with GarmentResponse and /swagger/v1/swagger.json.
export interface Garment {
  id: string
  name: string
  category: string
  subtype: string
  brand: string | null
  size: string | null
  notes: string | null
  status: string
  createdAt: string
  updatedAt: string
  colors: string[]
  seasons: string[]
  occasions: string[]
  styleTags: string[]
}

export async function getGarments(signal: AbortSignal): Promise<Garment[]> {
  const response = await fetch(`${apiBaseUrl}/api/garments`, { signal })
  if (!response.ok) throw new Error(`Request failed (${response.status})`)
  return response.json() as Promise<Garment[]>
}
